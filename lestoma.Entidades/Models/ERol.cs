using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models
{
    [Table("rol", Schema = "usuarios")]
    public partial class ERol
    {
        public ERol()
        {
            Usuarios = new HashSet<EUsuario>();
        }
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo requerido.")]
        [Column("nombre_rol")]
        public string NombreRol { get; set; }
        public ICollection<EUsuario> Usuarios { get; set; }
    }
}
