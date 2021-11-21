using System.ComponentModel.DataAnnotations;

namespace lestoma.CommonUtils.Requests
{
    public class RolRequest
    {
        [Required]
        public int IdUser { get; set; }

        [Required]
        public int RolUser { get; set; }

    }
}
