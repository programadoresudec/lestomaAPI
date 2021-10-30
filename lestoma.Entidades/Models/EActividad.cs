using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models
{
    [Table("actividad", Schema = "superadmin")]
    public class EActividad : ECamposAuditoria
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("nombre_actividad")]
        public string Nombre { get; set; }

    }
}
