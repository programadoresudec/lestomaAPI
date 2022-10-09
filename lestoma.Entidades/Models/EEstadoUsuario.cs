using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models
{
    [Table("estado_usuario", Schema = "usuarios")]
    public partial class EEstadoUsuario
    {
        public EEstadoUsuario()
        {
            Usuarios = new HashSet<EUsuario>();   
        }
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo requerido.")]
        [Column("descripcion")]
        public string DescripcionEstado { get; set; }
        public ICollection<EUsuario> Usuarios { get; set; }
    }
}
