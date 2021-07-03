using System.ComponentModel.DataAnnotations;

namespace lestoma.CommonUtils.Requests
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

}
