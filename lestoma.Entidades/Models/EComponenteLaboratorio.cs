using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models
{

    [Table("componente_laboratorio", Schema = "laboratorio_lestoma")]
    public partial class EComponenteLaboratorio : ECamposAuditoria
    {

        [Column("id")]
        public Guid Id { get; set; }
        [Column("modulo_componente_id")]
        public Guid ModuloComponenteId { get; set; }
        [Column("actividad_id")]
        public Guid ActividadId { get; set; }
        [Column("upa_id")]
        public Guid UpaId { get; set; }
        [Column("nombre_componente")]
        public string NombreComponente { get; set; }
        [Column("descripcion_estado", TypeName = "jsonb")]
        public string JsonEstadoComponente { get; set; }
        public EUpa Upa { get; set; }
        public EActividad Actividad { get; set; }
        public EModuloComponente ModuloComponente { get; set; }
    }
}
