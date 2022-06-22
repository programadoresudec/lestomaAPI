using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lestoma.CommonUtils.Requests
{
    public class CrearDetalleUpaActividadRequest
    {
        [Required(ErrorMessage = "El id de la upa es requerido")]
        public Guid UpaId { get; set; }
        [Required(ErrorMessage = "El id del usuario es requerido.")]
        public int UsuarioId { get; set; }
        [Required(ErrorMessage = "Una actividad es requerida.")]
        public List<ActividadRequest> Actividades { get; set; } = new List<ActividadRequest>();
    }
}
