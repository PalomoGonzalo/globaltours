using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Infraestructura.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        
        private readonly ApplicationDbContext _db;

        public LoginController(ApplicationDbContext db)
        {
            this._db = db;
           
        }

       
    }
}