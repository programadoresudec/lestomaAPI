using System.ComponentModel.DataAnnotations;

namespace lestoma.CommonUtils.Requests
{
    public class UsuarioRequest
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Clave { get; set; }
    }
}
