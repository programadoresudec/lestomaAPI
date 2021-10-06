using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models
{
    [Table("auditoria", Schema = "seguridad")]
    public class EAuditoria
    {

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("schema")]
        public string Schema { get; set; }
        [Column("tabla")]
        public string Tabla { get; set; }
        [Column("fecha")]
        public DateTime FechaGeneracion { get; set; }
        [Column("accion")]
        public string Accion { get; set; }
        [Column("user_bd")]
        public string UsuarioBD { get; set; }
        [Column("data", TypeName = "jsonb")]
        public string Data { get; set; }
        [Column("tipo_de_aplicacion")]
        public string TipoDeAplicacion { get; set; }
        [Column("ip")]
        public string Ip { get; set; }
        [Column("session")]
        public string Session { get; set; }
        [Column("PK")]
        public string PKTabla { get; set; }

    }
}
