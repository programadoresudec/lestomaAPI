using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models
{
    [Table("aplicacion", Schema = "seguridad")]
    public class EAplicacion
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("nombre")]
        public string NombreAplicacion { get; set; }
        [Column("tiempo_expiracion_token")]
        public short TiempoExpiracionToken { get; set; }
    }
}
