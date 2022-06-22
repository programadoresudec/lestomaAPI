using System.ComponentModel.DataAnnotations;

namespace lestoma.CommonUtils.Requests
{
    public class UsuarioRequest
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El apellido es requerido")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "la clave es requerida")]
        public string Clave { get; set; }
    }
}
