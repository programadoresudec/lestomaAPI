using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.CommonUtils.Entities
{
    [Table("estado_usuario", Schema = "usuarios")]
    public class EEstadoUsuario
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("descripcion")]
        public string DescripcionEstado { get; set; }
    }
}
