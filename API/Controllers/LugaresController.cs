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
        
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public LugaresController(IRepositorioGenerico<Lugar> lugarRepo, IMapper mapper,ApplicationDbContext db)
        {
            _mapper = mapper;
            _lugarRepo = lugarRepo;
            _db=db;

        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<LugarDTOS>>> GetLugares()
        {
            var espec = new LugaresConPaisCategoriaEspecificacion();
            var lugares = await _lugarRepo.ObtenerTodosEspec(espec);
            return Ok(_mapper.Map<IReadOnlyList<Lugar>, IReadOnlyList<LugarDTOS>>(lugares));

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LugarDTOS>> GetLugar(int id)
        {
            var espec = new LugaresConPaisCategoriaEspecificacion(id);
            var lugar = await _lugarRepo.ObtenerEspec(espec);
            var estadoMap=_mapper.Map<Lugar, LugarDTOS>(lugar);
            return Ok(estadoMap);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult>PutPrecio(int id, LugarPostDTOS lugarDto)
        {
            var auxLugar= await _db.Lugar.Where(x=>x.Id==id).SingleOrDefaultAsync();
            if (auxLugar==null)
            {
                return NotFound();
            }
            auxLugar.GastoAproximado=lugarDto.GastoAproximado;
            _db.Update(auxLugar);
            await _db.SaveChangesAsync();
            return NoContent();
        }


        [HttpPost]

        public async Task <ActionResult> CrearLugar 


    }
}