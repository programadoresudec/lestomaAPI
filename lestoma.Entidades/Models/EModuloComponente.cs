using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models
{
    [Table("modulo_componente", Schema = "laboratorio_lestoma")]
    public partial class EModuloComponente : ECamposAuditoria
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("nombre_modulo")]
        [Required(ErrorMessage = "Campo requerido.")]
        public string Nombre { get; set; }
    }
}
