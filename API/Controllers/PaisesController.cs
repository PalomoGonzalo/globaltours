using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entidades;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("[controller]")]
    public class PaisesController : Controller
    {
       private readonly IMapper _mapper;
        private readonly IRepositorioGenerico<Pais> _paisRepo;

        public PaisesController(IRepositorioGenerico<Pais> paisRepo, IMapper mapper)
        {
            _paisRepo = paisRepo;
            _mapper=mapper;
           
        }

        [HttpGet]
        public async Task<ActionResult<List<Pais>>> GetPaises()
        {
            return Ok(await _paisRepo.ObtenerTodosAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pais>> GetPaises(int id)
        {
            return Ok(await _paisRepo.ObtenerAsync(id));
        }

        
    }
}