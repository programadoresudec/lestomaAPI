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
        public int ModuloComponenteId { get; set; }
        [Column("actividad_id")]
        public Guid ActividadId { get; set; }
        [Column("upa_id")]
        public Guid UpaId { get; set; }
        [Column("nombre_componente")]
        public string NombreComponente { get; set; }
        [Column("descripcion_estado", TypeName = "Json")]
        public string JsonEstadoComponente { get; set; }
    }
}
