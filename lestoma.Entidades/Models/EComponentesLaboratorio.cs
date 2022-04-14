using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace lestoma.Entidades.Models
{

    [Table("componente_laboratorio", Schema = "laboratorio_lestoma")]
    public partial class EComponentesLaboratorio : ECamposAuditoria
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("modulo_componente_id")]
        public int ModuloComponenteId { get; set; }
        [Column("actividad_id")]
        public Guid ActividadId { get; set; }
        [Column("nombre")]
        public string Nombre { get; set; }
        [Column("tipos_estado_componente", TypeName = "Json")]
        public string TiposEstadoComponente { get; set; }
    }
}
