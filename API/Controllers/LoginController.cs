using System.Security.Claims;
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
using System.IdentityModel.Tokens.Jwt;

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

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTOS login)
        {
            if (login.Clave is null && login.User is null)
            {
                return BadRequest("Error datos incorrectos");
            }

            Usuario usuario = await _db.Usuario.Where(x => x.User == login.User).FirstOrDefaultAsync();

            if (usuario == null)
            {
                return NotFound("Usuario o contaseña invalidas");
            }

            if (HashHelper.CheckHash(login.Clave, usuario.Clave, usuario.Sal))
            {
                var secretKey=_configuration.GetValue<string>("SecretKey");
                var key = Encoding.ASCII.GetBytes(secretKey);

                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, login.User));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddHours(4),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);

                string bearerToken = tokenHandler.WriteToken(createdToken);
                return Ok(bearerToken);
                
            }
            else
            {
                 return NotFound("Usuario o contaseña invalidas");
            }     
        }
    }
}