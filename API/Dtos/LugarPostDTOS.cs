using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class LugarPostDTOS
    {
       
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public double GastoAproximado { get; set; }

        public IFormFile ImagenUrl {get; set; }

        public int IdPais{get; set;}

        public int IdCategoria { get; set; }

        public int Valoracion{ get; set;}

      
    }
}