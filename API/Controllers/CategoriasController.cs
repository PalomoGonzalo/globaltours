using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("[controller]")]
    public class CategoriasController : Controller
    {

        private readonly IRepositorioGenerico<Categoria> _categoriaRepo;

        public CategoriasController(IRepositorioGenerico<Categoria> categoriaRepo)
        {
            _categoriaRepo = categoriaRepo;

        }

        [HttpGet("categorias")]
        public async Task<ActionResult<List<Categoria>>> GetCategorias()
        {
            return Ok(await _categoriaRepo.ObtenerTodosAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategorias(int id)
        {
            return Ok(await _categoriaRepo.ObtenerAsync(id));
        }


        

    }
}