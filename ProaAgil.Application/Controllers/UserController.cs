using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProAgil.Domain.Dtos;
using ProAgil.Domain.Identity;

namespace ProaAgil.Application.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        public readonly SignInManager<User> _SignInManager;
        public readonly IMapper _Mapper;

        public UserController (IConfiguration config, UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper) {
            _Mapper = mapper;
            _SignInManager = signInManager;
            _config = config;
            _userManager = userManager;
        }

        [HttpGet ("GetUser")]
        public async Task<IActionResult> GetUser () {
            return Ok (new UserDto());
        }

        [HttpPost ("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register (UserDto userDto) {
            var usuario = _Mapper.Map<User> (userDto);
            var resultado = await _userManager.CreateAsync (usuario, userDto.Password);
            var usuarioParaRetorno = _Mapper.Map<UserDto> (usuario);

            if (resultado.Succeeded) {
                return Created ("GetUser", usuarioParaRetorno);
            } else {
                return BadRequest (resultado.Errors);
            }

        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var usuario = await _userManager.FindByNameAsync(userLoginDto.UserName);
            var resultado = await _SignInManager.CheckPasswordSignInAsync(usuario, userLoginDto.Password,false);

            if(resultado.Succeeded)
            {
                var appUser = await _userManager.Users
                    .FirstOrDefaultAsync(u => u.NormalizedUserName == userLoginDto.UserName.ToUpper());
                
                var usuarioParaRetorno = _Mapper.Map<UserLoginDto>(appUser);
                return Ok(new {
                    token = GenerateJwtToken(appUser).Result,
                    user = usuarioParaRetorno
                });
            }

            return Unauthorized();
        }

        private async Task<string> GenerateJwtToken(User user )
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            
            };
            
            var roles = await _userManager.GetRolesAsync(user);

            foreach(var role in roles)
            {
                //pego todos os papeis e coloco em roles
                claims.Add(new Claim(ClaimTypes.Role,role));
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII
                 .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}