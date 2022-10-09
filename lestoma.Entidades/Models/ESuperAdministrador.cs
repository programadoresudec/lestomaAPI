using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models
{
    [Table("super_administrador", Schema = "superadmin")]
    public partial class ESuperAdministrador
    {
        [Key]
        [Column("id")]
        public short Id { get; set; }
        [Column("usuario_id")]
        [Required(ErrorMessage = "Campo requerido.")]
        public short UsuarioId { get; set; }
        [NotMapped]
        public EUsuario Admin { get; set; }
    }
}
