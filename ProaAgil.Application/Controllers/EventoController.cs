using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Domain.Dtos;
using ProAgil.Repository.Interface;

namespace ProaAgil.Application.Controllers 
{
     [Route("api/[controller]")]
     [ApiController]
    public class EventoController : ControllerBase
    {
        public readonly IRepositoryBase _repository;
        private readonly IMapper _mapper;

        public EventoController (IRepositoryBase repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodosEventos () 
        {
            var resultados = await _repository.GetAllEventoAsync (true);
            var retorno = _mapper.Map<IEnumerable<EventoDto>>(resultados);
            return Ok (retorno);
        }

     [HttpPost("upload")]
        public async Task<IActionResult> Upload()
        {
            
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                    var fullPath = Path.Combine(pathToSave, filename.Replace("\"", " ").Trim());

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        //gera o arquivo
                        file.CopyTo(stream);
                    }
                }

                return Ok();
       
    
        }
       

        [HttpGet ("{eventoId}")]
        public async Task<IActionResult> ObterEventoPorId (int eventoId) 
        {
            var resultado = await _repository.GetEventoAsyncById (eventoId, true);
            var retorno = _mapper.Map<EventoDto>(resultado);
            return Ok (retorno);
        }

        [HttpGet ("getByTema/{tema}")]
        public async Task<IActionResult> ObterEventosPorTema (string tema) 
        {
            var eventos = await _repository.GetAllEventoAsyncByTema (tema, true);
            var resultados = _mapper.Map<IEnumerable<EventoDto>>(eventos);
            return Ok (resultados);
        }

        [HttpPost]
  
        public async Task<IActionResult> AdicionarEventos (EventoDto eventoExterno) 
        {
            var eventoParaAdicionar = _mapper.Map<Evento>(eventoExterno);
            _repository.Add (eventoParaAdicionar);
            if (await _repository.SaveChangesAsync ())
             {
                return Created ($"/api/evento/{eventoExterno.Id}", eventoExterno);
            } 
            else 
            {
                return StatusCode(412);
            }

        }

        [HttpPut("{EventoId}")]
        public async Task<IActionResult> AlterarDadosDoEvento (int eventoId, EventoDto evento) 
        {
            var eventosDoRepositorio = await _repository.GetEventoAsyncById (eventoId, false);
            if (eventosDoRepositorio == null) 
            {
                return NotFound ();
            } 
            else 
            {
                _mapper.Map(evento,eventosDoRepositorio);
                _repository.Update (eventosDoRepositorio);
                await _repository.SaveChangesAsync ();
                return Ok (eventosDoRepositorio);

            }
        }

        [HttpDelete("{eventoId}")]
        public async Task<IActionResult> DeletarRegistro(int eventoId)
        {
            var resultado = await _repository.GetEventoAsyncById(eventoId,false);
            if(resultado ==null)
            {
                return BadRequest();
            }
            else
            {
                _repository.Delete(resultado);
                await _repository.SaveChangesAsync ();
                return Ok ();
            }
        }

    }
    }
