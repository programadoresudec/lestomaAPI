using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.CommonUtils.Entities
{
    [Table("usuario", Schema = "usuarios")]
    public class EUsuario
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("nombre")]
        public string Nombre { get; set; }
        [Column("apellido")]
        public string Apellido { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("clave")]
        public string Clave { get; set; }
        [Column("codigo_recuperacion")]
        public string CodigoRecuperacion { get; set; }
        [Column("vencimiento_codigo_recuperacion")]
        public DateTime? FechaVencimientoCodigo { get; set; }
        [Column("rol_id")]
        public int RolId { get; set; }
        [Column("estado_id")]
        public int EstadoId { get; set; }

        [NotMapped]
        public EEstadoUsuario EstadoUsuario { get; set; } = new EEstadoUsuario();
        [NotMapped]
        public ERol Rol { get; set; } = new ERol();

    }
}
