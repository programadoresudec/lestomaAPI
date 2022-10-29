using System.ComponentModel.DataAnnotations;

namespace lestoma.CommonUtils.Requests
{
    public class ChangeProfileRequest
    {
        [Required(ErrorMessage = "El id es requerido")]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "El apellido es requerido")]
        public string Apellidos { get; set; }
    }
}
