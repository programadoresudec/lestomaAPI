using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lestoma.CommonUtils.Requests
{
    public class CrearDetalleUpaActividadRequest
    {
        [Required]
        public Guid UpaId { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public List<ActividadRequest> Actividades { get; set; } = new List<ActividadRequest>();
    }
}
