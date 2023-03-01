using lestoma.CommonUtils.DTOs;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models
{
    [Table("buzon", Schema = "reportes")]
    public partial class EBuzon
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo requerido.")]
        [Column("descripcion", TypeName = "jsonb")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Campo requerido.")]
        [Column("usuario_id")]
        public int UsuarioId { get; set; }
        [Column("ip")]
        [Required(ErrorMessage = "Campo requerido.")]
        public string Ip { get; set; }
        [Column("session")]
        [Required(ErrorMessage = "Campo requerido.")]
        public string Session { get; set; }
        [Column("tipo_de_aplicacion")]
        [Required(ErrorMessage = "Campo requerido.")]
        public string TipoDeAplicacion { get; set; }
        [Column("fecha_creacion_server")]
        [Required(ErrorMessage = "Campo requerido.")]
        public DateTime FechaCreacionServer { get; set; }
        [NotMapped]
        public UserDTO User { get; set; } = new UserDTO();
    }
}
