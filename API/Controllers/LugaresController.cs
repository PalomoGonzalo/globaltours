using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;
using Core.Especificaciones;
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
        
        private readonly IRepositorioGenerico<Lugar> _lugarRepo;
        private readonly IRepositorioGenerico<Categoria> _categoriaRepo;
        private readonly IRepositorioGenerico<Pais> _paisRepo;
        
        public LugaresController(IRepositorioGenerico<Lugar> lugarRepo,IRepositorioGenerico<Pais> paisRepo, IRepositorioGenerico<Categoria> categoriaRepo)
        {
            _paisRepo = paisRepo;
            _categoriaRepo = categoriaRepo;
            _lugarRepo = lugarRepo;
           
        }

        [HttpGet]
        public async Task< ActionResult<List<Lugar>>> GetLugares()
        {
            var espec=new LugaresConPaisCategoriaEspecificacion();
            var lugares= await _lugarRepo.ObtenerTodosEspec(espec);
            return Ok(lugares);
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Lugar>> GetLugar(int id)
        {
            var espec=new LugaresConPaisCategoriaEspecificacion(id);
            return await _lugarRepo.ObtenerEspec(espec); 
        }

        [HttpGet("paises")]
        public async Task<ActionResult<List<Pais>>> GetPaises()
        {
            return Ok(await _paisRepo.ObtenerTodosAsync());
        }

         [HttpGet("categorias")]
        public async Task<ActionResult<List<Pais>>> GetCategorias()
        {
            return Ok(await _categoriaRepo.ObtenerTodosAsync());
        }

    

    }
}