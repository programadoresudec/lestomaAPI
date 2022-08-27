using System;
using System.ComponentModel.DataAnnotations;

namespace lestoma.CommonUtils.Requests.Filters
{
    public class UpaUserFilterRequest
    {
        [Required(ErrorMessage = "El id de la upa es requerido")]
        public Guid UpaId { get; set; }
        [Required(ErrorMessage = "El id de usuario es requerido")]
        public int UsuarioId { get; set; }
    }
}
