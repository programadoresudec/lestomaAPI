using System.ComponentModel.DataAnnotations;

namespace lestoma.CommonUtils.DTOs
{
    public class TokenRefreshDTO
    {
        [Required(ErrorMessage = "El token es requerido")]
        public string Token { get; set; }
    }
}
