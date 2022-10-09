using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models
{
    public class EReporte
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("detalle", TypeName = "jsonb")]
        [Required(ErrorMessage = "Campo requerido.")]
        public string DetalleJson { get; set; }
        [Column("detalle_laboratorio_id")]
        [Required(ErrorMessage = "Campo requerido.")]
        public Guid DetalleLaboratorioId { get; set; }
        [Required(ErrorMessage = "Campo requerido.")]
        [Column("anterior_registro_id")]
        public Guid AnteriorRegistroId { get; set; }
    }
}
