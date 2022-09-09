using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Helpers;
using Core.Entidades;
using Infraestructura.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [Route("[controller]")]
    public class LoginController : Controller
    {

        private readonly ApplicationDbContext _db;
        public IConfiguration _configuration { get; }

        public LoginController(ApplicationDbContext db, IConfiguration configuration)
        {
            this._configuration = configuration;
            this._db = db;
        }

        public async Task<IActionResult> Login(LoginDTOS login)
        {
            if (login.Clave is null && login.User is null)
            {
                return BadRequest("Error datos incorrectos");
            }

            Usuario usuario = await _db.Usuario.Where(x => x.User == login.User).FirstOrDefaultAsync();

            if (usuario != null)
            {
                return NotFound();
            }

            if (HashHelper.CheckHash(login.Clave, usuario.Clave, usuario.Sal))
            {
                var secretKey=_configuration.GetValue<string>("SecretKey");
                
            }
            else
            {
                return Forbid();
            }
            return Ok();
        }


        

    }
}