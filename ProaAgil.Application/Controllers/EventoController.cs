using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository.Interface;

namespace ProaAgil.Application.Controllers 
{
     [Route("api/[controller]")]
      [ApiController]
    public class EventoController : ControllerBase
    {
        public readonly IRepositoryBase _repository;
        public EventoController (IRepositoryBase repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodosEventos () 
        {
            var resultados = await _repository.GetAllEventoAsync (true);
            return Ok (resultados);
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> ObterEventoPorId (int eventoId) 
        {
            var resultados = await _repository.GetEventoAsyncById (eventoId, true);
            return Ok (resultados);
        }

        [HttpGet ("getByTema/{tema}")]
        public async Task<IActionResult> ObterEventosPorTema (string tema) 
        {
            var resultados = await _repository.GetAllEventoAsyncByTema (tema, true);
            return Ok (resultados);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarEventos (Evento evento) 
        {
           
     
            _repository.Add (evento);
            if (await _repository.SaveChangesAsync ()) {
                return Created ($"/api/evento/{evento.Id}", evento);
            } 
            else {
                return StatusCode(412);
            }

        }

        [HttpPut("{EventoId}")]
        public async Task<IActionResult> AlterarDadosDoEvento (int eventoId, Evento evento) 
        {
            var resultados = await _repository.GetEventoAsyncById (eventoId, false);
            if (resultados == null) 
            {
                return NotFound ();
            } 
            else 
            {
                _repository.Update (evento);
                await _repository.SaveChangesAsync ();
                return Ok (evento);

            }
        }

        [HttpDelete]
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
                return Ok ();
            }
        }

    }
    }
