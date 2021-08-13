using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace lestoma.Entidades.Models
{
    [Owned]
    [Table("tokens_usuario_por_aplicacion", Schema = "seguridad")]
    public class ETokensUsuarioByAplicacion
    {
        [Key]
        [Column("id")]
        [JsonIgnore]
        public int Id { get; set; }
        [Column("token")]
        public string Token { get; set; }
        [Column("expiracion")]
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        [Column("fecha_creacion")]
        public DateTime Created { get; set; }
        [Column("creado_por_ip")]
        public string CreatedByIp { get; set; }
        [Column("fecha_revocacion")]
        public DateTime? Revoked { get; set; }
        [Column("revocado_por_ip")]
        public string RevokedByIp { get; set; }
        [Column("reeemplazado_por_token")]
        public string ReplacedByToken { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;
        [Column("aplicacion_id")]
        public int AplicacionId { get; set; }
        [NotMapped]
        public int UsuarioId { get; set; }
    }
}
