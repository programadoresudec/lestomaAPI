using System;
using System.ComponentModel.DataAnnotations;

namespace lestoma.CommonUtils.Requests
{
    public class ModuloRequest
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
    }
}