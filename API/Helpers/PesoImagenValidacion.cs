using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class PesoImagenValidacion: ValidationAttribute
    {
        private readonly int _PesoMaximoEnMegabytes;
        public PesoImagenValidacion(int PesoMaximoEnMegabytes)
        {
            _PesoMaximoEnMegabytes = PesoMaximoEnMegabytes;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }


            IFormFile formFile = value as IFormFile;

            if (formFile == null)
            {
              return ValidationResult.Success;  
            }

            if (formFile.Length> _PesoMaximoEnMegabytes*1024*1024)
            {
                return new ValidationResult($"El peso del archivo no debe ser mayor a {_PesoMaximoEnMegabytes}");
            }

            return ValidationResult.Success;
        }

        
    }
}