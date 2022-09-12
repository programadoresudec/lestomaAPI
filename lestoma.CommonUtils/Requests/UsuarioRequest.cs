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
        [MaxLength(30, ErrorMessage = "La clave debe contener maximo 30 caracteres"), MinLength(8, ErrorMessage = "La clave debe contener minimo 8 caracteres")]
        [Required(ErrorMessage = "la clave es requerida")]

        public string Clave { get; set; }
    }
}
