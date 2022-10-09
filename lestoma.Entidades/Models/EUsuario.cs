using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models
{
    [Table("usuario", Schema = "usuarios")]
    public partial class EUsuario : ECamposAuditoria
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("nombre")]
        [Required(ErrorMessage = "Campo requerido.")]
        public string Nombre { get; set; }
        [Column("apellido")]
        [Required(ErrorMessage = "Campo requerido.")]
        public string Apellido { get; set; }
        [Column("email")]
        [Required(ErrorMessage = "Campo requerido.")]
        public string Email { get; set; }
        [Column("clave")]
        [Required(ErrorMessage = "Campo requerido.")]
        public string Clave { get; set; }
        [Column("codigo_recuperacion")]
        public string CodigoRecuperacion { get; set; }
        [Column("vencimiento_codigo_recuperacion")]
        public DateTime? FechaVencimientoCodigo { get; set; }
        [Column("rol_id")]
        [Required(ErrorMessage = "Campo requerido.")]
        public int RolId { get; set; }
        [Column("estado_id")]
        [Required(ErrorMessage = "Campo requerido.")]
        public int EstadoId { get; set; }
        [Column("semilla")]
        [Required(ErrorMessage = "Campo requerido.")]
        public string Salt { get; set; }
        public EEstadoUsuario EstadoUsuario { get; set; }
        public ERol Rol { get; set; }
    }
}
