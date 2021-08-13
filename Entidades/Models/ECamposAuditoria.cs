using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models
{
    public class ECamposAuditoria
    {
        [Column("ip")]
        public string Ip { get; set; }
        [Column("session")]
        public string Session { get; set; }
        [Column("tipo_de_aplicacion")]
        public string TipoDeAplicacion { get; set; }
    }
}