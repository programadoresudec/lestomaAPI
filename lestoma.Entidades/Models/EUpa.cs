using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models
{
    [Table("upa", Schema = "superadmin")]
    public partial class EUpa : ECamposAuditoria
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("nombre_upa")]
        [Required(ErrorMessage = "Campo requerido.")]
        public string Nombre { get; set; }
        [Column("superadmin_id")]
        [Required(ErrorMessage = "Campo requerido.")]
        public int SuperAdminId { get; set; }
        [Column("descripcion")]
        [Required(ErrorMessage = "Campo requerido.")]
        public string Descripcion { get; set; }
        [Column("cantidad_actividades")]
        [Required(ErrorMessage = "Campo requerido.")]
        public short CantidadActividades { get; set; }
    }
}
