using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Errores;
using Infraestructura.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
       
        private readonly ApplicationDbContext _db;

        public TestController(ApplicationDbContext db)
        {
            _db=db;
        }

       [HttpGet("notfound")]
       public ActionResult GetNotFoundError()
       {
            var algo= _db.Lugar.Find(166);
            if(algo==null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok();
       }

       [HttpGet("servererror")]
       public ActionResult GetServerError()
       {
            var algo= _db.Lugar.Find(166);
            var algoARetonar= algo.ToString();
            return Ok();
       }

       [HttpGet("badresquest")]
       public ActionResult GetBadResquest()
       {
           return BadRequest(new ApiResponse(400));
       }

       [HttpGet("badresquest/{id}")]
       public ActionResult GetBadResquest(int id)
       {
           return BadRequest();
       }



    }
}