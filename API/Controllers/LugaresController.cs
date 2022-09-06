using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
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
        private readonly IMapper _mapper;
        
        public LugaresController(IRepositorioGenerico<Lugar> lugarRepo,IRepositorioGenerico<Pais> paisRepo, IRepositorioGenerico<Categoria> categoriaRepo,IMapper mapper)
        {
            _mapper = mapper;
            _paisRepo = paisRepo;
            _categoriaRepo = categoriaRepo;
            _lugarRepo = lugarRepo;
           
        }

        [HttpGet]
        public async Task< ActionResult<IReadOnlyList<LugarDTOS>>> GetLugares()
        {
            var espec=new LugaresConPaisCategoriaEspecificacion();
            var lugares= await _lugarRepo.ObtenerTodosEspec(espec);
            return Ok(_mapper.Map<IReadOnlyList<Lugar>,IReadOnlyList<LugarDTOS>>(lugares));
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LugarDTOS>> GetLugar(int id)
        {
            var espec=new LugaresConPaisCategoriaEspecificacion(id);
            var lugar= await _lugarRepo.ObtenerEspec(espec); 
            return _mapper.Map<Lugar,LugarDTOS>(lugar);
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