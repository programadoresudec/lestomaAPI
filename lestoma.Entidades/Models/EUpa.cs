using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models
{
    [Table("upa", Schema = "superadmin")]
    public partial class EUpa : ECamposAuditoria
    {
        public EUpa()
        {
            ProtocolosCOM = new HashSet<EProtocoloCOM>();
        }
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
        [StringLength(5000, MinimumLength = 6, ErrorMessage = "Minimo 10 caracteres y maximo 5000 caracteres")]
        public string Descripcion { get; set; }
        [Column("cantidad_actividades")]
        [Range(1, 50, ErrorMessage = "El valor debe ser mayor que 0")]
        [Required(ErrorMessage = "Campo requerido.")]
        public short CantidadActividades { get; set; }
        public ICollection<EProtocoloCOM>  ProtocolosCOM { get; set; }
    }
}
