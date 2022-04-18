using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models
{
    [Table("usuario", Schema = "usuarios")]
    public class EUsuario : ECamposAuditoria
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
        [Column("semilla")]
        public string Salt { get; set; }
    
        public EEstadoUsuario EstadoUsuario { get; set; } 
        public ERol Rol { get; set; }

        [NotMapped]
        public string RefreshToken { get; set; }
        [JsonIgnore]
        public List<ETokensUsuarioByAplicacion> RefreshTokens { get; set; }
        [NotMapped]
        public int AplicacionId { get; set; }


        [NotMapped]
        public string Nombre_rol { get; set; }
    }
}
