using System.ComponentModel.DataAnnotations;

namespace lestoma.CommonUtils.Requests
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Clave { get; set; }
        [Required]
        public int TipoAplicacion { get; set; }
        public string Ip { get; set; }
    }
    public class TipoAplicacionRequest
    {
        [Required]
        public int TipoAplicacion { get; set; }
    }
}
