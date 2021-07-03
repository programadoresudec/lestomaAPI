using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.CommonUtils.Entities
{
    [Table("rol", Schema = "usuarios")]
    public class ERol
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("nombre_rol")]
        public string NombreRol { get; set; }
    }
}
