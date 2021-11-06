using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models
{

    [Table("recirculacion_de_agua", Schema = "laboratorio_lestoma")]

    public class ERecirculacionAgua : ECamposAuditoria
    {
        [Key]
        [Column("id")]

        public Guid Id {get; set;}
        [Column("detalle", TypeName ="Json")]
        public string detalle { get; set;}
        [Column("detalle_laboratorio_id")]
        public int detalleLaboratorio {get; set;}
        [Column("anterior_cambio_id")]
        public Guid AnteriorCambio{ get; set;}
    }
    
}