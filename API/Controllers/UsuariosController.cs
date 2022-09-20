using System.Reflection.Metadata.Ecma335;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Helpers;
using Core.Entidades;
using Core.Interfaces;
using Infraestructura.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace API.Controllers
{
    [Route("[controller]")]
    public class UsuariosController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly IRepositorioGenerico<Usuario> _usuarioRepo;
        

        public UsuariosController(ApplicationDbContext db, IRepositorioGenerico<Usuario> usuarioRepo)
        {
            this._usuarioRepo = usuarioRepo;
        
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody]Usuario usuario)
        {
            if (usuario.User== null && usuario.Clave==null)
            {
                return BadRequest("Error faltan datos");
            }

            if (await _db.Usuario.Where(x => x.User == usuario.User).AnyAsync())
            {
                return BadRequest("Error el usuario ya existe");
            }

            hashedPassword Password = HashHelper.Hash(usuario.Clave);
            usuario.Clave = Password.Password;
            usuario.Sal = Password.Salt;
            _db.Usuario.Add(usuario);
            await _db.SaveChangesAsync();
            return Ok(new UsuarioDTOS
            {
                Id = usuario.Id,
                User = usuario.User
            });
          
        }


        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> GetUsuarios()
        {
            return Ok(await _usuarioRepo.ObtenerTodosAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetPaises(int id)
        {
            return Ok(await _usuarioRepo.ObtenerAsync(id));
        }
    }
}