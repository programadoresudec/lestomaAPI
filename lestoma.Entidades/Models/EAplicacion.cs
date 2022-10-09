using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models
{
    [Table("aplicacion", Schema = "seguridad")]
    public partial class EAplicacion
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("nombre")]
        [Required(ErrorMessage = "Campo requerido.")]
        public string NombreAplicacion { get; set; }
        [Required(ErrorMessage = "Campo requerido.")]
        [Column("tiempo_expiracion_token")]
        public short TiempoExpiracionToken { get; set; }
    }
}
