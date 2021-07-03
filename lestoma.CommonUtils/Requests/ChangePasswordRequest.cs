using System.ComponentModel.DataAnnotations;

namespace lestoma.CommonUtils.Requests
{
    public class ChangePasswordRequest
    {
        [Required]
        public int IdUser { get; set; }
        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }


    }
}
