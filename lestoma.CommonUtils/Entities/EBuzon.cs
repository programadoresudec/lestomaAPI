using lestoma.CommonUtils.Responses;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.CommonUtils.Entities
{
    [Table("buzon", Schema = "reportes")]
    public class EBuzon
    {

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("fecha_creacion")]
        public DateTime FechaCreacion { get; set; }
        [Column("descripcion", TypeName = "json")]
        public string Descripcion { get; set; }
        [Column("usuario_id")]
        public int UsuarioId { get; set; }
        [NotMapped]
        public UserResponse User { get; set; } = new UserResponse();
    }
}
