using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models
{
    [Table("modulo_componente", Schema = "laboratorio_lestoma")]
    public class EModuloComponente : ECamposAuditoria
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("nombre_modulo")]
        public string Nombre { get; set; }
    }
}
