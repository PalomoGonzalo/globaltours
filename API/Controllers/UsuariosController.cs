using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using Infraestructura.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("[controller]")]
    public class UsuariosController : Controller
    {
        
        private readonly ApplicationDbContext _db;

        public UsuariosController(ApplicationDbContext db)
        {
            _db = db;
           
        }

      /*  [HttpPost]
        public async Task<IActionResult> CrearUsuario(UsuarioDTOS usuario)
        {
            if(ModelState.IsValid)
            {
                return BadRequest("Error faltan datos");
            }

            if(await _db.Usuario.Where(x=>x.User==usuario.User).AnyAsync())
            {
                return BadRequest("Error el usuario ya existe");
            }
            

        }

       */
    }
}