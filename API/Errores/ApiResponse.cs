using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errores
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string mensaje=null)
        {
            StatusCode = statusCode;
            Mensaje = mensaje??MensajeStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Mensaje { get; set; }

        private string MensajeStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400=>"un Bad request se ha realizado",
                401=>"No estas Autorizado",
                404=>"Recurso No encontrado",
                500=>"Error interno, Comunicarse con el Administrador",
                _=>null
            };
        }
    }
}