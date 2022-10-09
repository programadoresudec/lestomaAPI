using lestoma.CommonUtils.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models
{
    [Table("upa_actividad", Schema = "superadmin")]
    public partial class EUpaActividad : ECamposAuditoria
    {
        [Column("upa_id")]
        [Required(ErrorMessage = "Campo requerido.")]
        public Guid UpaId { get; set; }
        [Column("actividad_id")]
        [Required(ErrorMessage = "Campo requerido.")]
        public Guid ActividadId { get; set; }
        [Column("usuario_id")]
        [Required(ErrorMessage = "Campo requerido.")]
        public int UsuarioId { get; set; }
        public EUpa Upa { get; set; }
        public EActividad Actividad { get; set; }
        public EUsuario Usuario { get; set; }
    }
}
