using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models
{
    [Table("actividad", Schema = "superadmin")]
    public class EActividad
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("nombre_actividad")]
        public string Nombre { get; set; }
        public List<EUpaActividad> Actividades { get; set; }
    }
}
