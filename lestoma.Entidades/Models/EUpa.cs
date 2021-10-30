using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models
{
    [Table("upa", Schema = "superadmin")]
    public class EUpa : ECamposAuditoria
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("nombre_upa")]
        public string Nombre { get; set; }
        [Column("superadmin_id")]
        public int SuperAdminId { get; set; }
        [Column("descripcion")]
        public string Descripcion { get; set; }
        [Column("cantidad_actividades")]
        public short CantidadActividades { get; set; }
    }
}
