using System;
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
        [Required(ErrorMessage = "Campo requerido.")]
        public string Nombre { get; set; }
        [Column("upa_id")]
        [Required(ErrorMessage = "Campo requerido.")]
        public Guid UpaId { get; set; }
        [Column("primer_byte_trama", TypeName = "smallint")]
        [Required(ErrorMessage = "Campo requerido.")]
        public byte PrimerByteTrama { get; set; }
        [Column("sigla")]
        [Required(ErrorMessage = "Campo requerido.")]
        [StringLength(5, MinimumLength = 2, ErrorMessage = "Minimo 2 caracteres y maximo 5 caracteres")]
        public string Sigla { get; set; }
        public EUpa Upa { get; set; }

    }
}
