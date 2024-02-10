using lestoma.CommonUtils.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace lestoma.CommonUtils.Core.Attributes
{
    public sealed class TipoArchivoAttribute : ValidationAttribute
    {
        private readonly string[] tiposValidos;

        public TipoArchivoAttribute(string[] tiposValidos)
        {
            this.tiposValidos = tiposValidos;
        }

        public TipoArchivoAttribute(GrupoTipoArchivo grupoTipoArchivo)
        {
            if (grupoTipoArchivo == GrupoTipoArchivo.IMAGEN)
            {
                tiposValidos = new string[] { "image/jpeg", "image/png", "image/jpg", "image/bmp", "image/gif" };
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }


            if (!(value is IFormFile formFile))
            {
                return ValidationResult.Success;
            }

            if (!tiposValidos.Contains(formFile.ContentType))
            {
                return new ValidationResult($"El tipo del archivo debe ser uno de los siguientes: {string.Join(", ", tiposValidos)}");
            }

            return ValidationResult.Success;
        }
    }
}
