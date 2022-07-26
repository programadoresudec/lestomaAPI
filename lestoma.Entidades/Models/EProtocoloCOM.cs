using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models
{
    [Table("protocolo_com", Schema = "laboratorio_lestoma")]
    public partial class EProtocoloCOM
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("nombre")]
        public string Nombre { get; set; }
        [Column("primer_byte_trama")]
        public string PrimerByteTrama { get; set; }
        [Column("sigla")]
        public string Sigla { get; set; }
    }
}
