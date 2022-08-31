using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;
using Core.Interfaces;
using Infraestructura.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LugaresController : ControllerBase
    {
        private readonly ILugarRepositorio _repositorio;
        
        public LugaresController(ILugarRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public async Task< ActionResult<List<Lugar>>> GetLugares()
        {
            var lugares= await _repositorio.GetLugaresAsync();
            return Ok(lugares);
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Lugar>> GetLugar(int id)
        {
            return await _repositorio.GetLugarAsync(id); 
        }

    }
}