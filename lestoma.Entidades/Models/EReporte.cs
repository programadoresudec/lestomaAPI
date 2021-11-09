using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models
{
    public class EReporte
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("detalle", TypeName = "Json")]
        public string DetalleJson { get; set; }
        [Column("detalle_laboratorio_id")]
        public Guid DetalleLaboratorioId { get; set; }
        [Column("anterior_registro_id")]
        public Guid AnteriorRegistroId { get; set; }
    }
}
