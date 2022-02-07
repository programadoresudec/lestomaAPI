using System.ComponentModel.DataAnnotations;

namespace lestoma.CommonUtils.Requests
{
    public class ChangeProfileRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nombres { get; set; }
        [Required]
        public string Apellidos { get; set; }
    }
}
