using System.ComponentModel.DataAnnotations;

namespace lestoma.CommonUtils.Requests
{
    public class ModuloRequest
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
    }
}