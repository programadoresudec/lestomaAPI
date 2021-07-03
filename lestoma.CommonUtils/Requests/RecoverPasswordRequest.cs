using System.ComponentModel.DataAnnotations;

namespace lestoma.CommonUtils.Requests
{
    public class RecoverPasswordRequest
    {
        [Required]
        public string Codigo { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
