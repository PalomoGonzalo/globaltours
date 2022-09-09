using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class UsuarioDTOS
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El usuario no puede estar vacio")]
        public string User { get; set; }

        [Required(ErrorMessage = "La contraseña no puede estar vacio")]
        public string Clave { get; set; }

        public string Sal { get; set; }
    }
}