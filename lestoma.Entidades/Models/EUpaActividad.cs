using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models
{
    [Table("upa_actividad", Schema = "superadmin")]
    public class EUpaActividad : ECamposAuditoria
    {
        [Column("upa_id")]
        public int UpaId { get; set; }
        [Column("actividad_id")]
        public int ActividadId { get; set; }
        [Column("usuario_id")]
        public int UsuarioId { get; set; }
        public EUpa Upa { get; set; }
        public EActividad Actividad { get; set; }
        [Column("descripcion")]
        public string Descripcion { get; set; }
    }
}
