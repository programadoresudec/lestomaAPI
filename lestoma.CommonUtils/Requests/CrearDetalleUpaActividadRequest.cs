using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace lestoma.CommonUtils.Requests
{
    public class CrearDetalleUpaActividadRequest
    {
        [Required]
        public int UpaId { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public List<ActividadRequest> Actividades { get; set; } = new List<ActividadRequest>();
    }
}
