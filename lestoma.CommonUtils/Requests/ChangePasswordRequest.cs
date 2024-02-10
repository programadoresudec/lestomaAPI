using System.ComponentModel.DataAnnotations;

namespace lestoma.CommonUtils.Requests
{
    public class ChangePasswordRequest
    {
        [Required(ErrorMessage = "El id de usuario es requerido")]
        public int IdUser { get; set; }
        [Required(ErrorMessage = "la contraseña antigua es requerida")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "la contraseña nueva es requerida")]
        public string NewPassword { get; set; }
    }
}
