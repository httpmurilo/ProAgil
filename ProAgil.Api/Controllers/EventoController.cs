using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Api.Data;
using ProAgil.Api.Model;

namespace ProAgil.Api.Controllers
{
    [ApiController]
    public class EventoController : ControllerBase 
    {

        private readonly DataContext _context;
        public EventoController(DataContext context)
        {
            context = _context;
            
        }
    }
}