using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProaAgil.Repository.Data;

namespace ProaAgil.Application.Controllers
{
    
    [Route("api/[controller]")]
      [ApiController]
    public class ValuesController : ControllerBase
    {
         public readonly DataContext _context ;

        public ValuesController (DataContext context) 
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await _context.Eventos.ToListAsync();
            return Ok(results);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Get (int id)
        {
            var results = await _context.Eventos.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(results);
        }
    }
}