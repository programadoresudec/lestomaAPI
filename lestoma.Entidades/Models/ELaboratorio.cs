using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models

{

    [Table("detalle_laboratorio", Schema = "laboratorio_lestoma")]

    public class Elaboratorio : ECamposAuditoria
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("tipo_estado_componente_id")]
        public int TipoEstadoComponenteId{ get; set;}
        [Column("componente_laboratorio_id")] 
        public int ComponenteLaboratorioId{ get; set;}
        [Column("tipo_com_id")]
        public int TipoDeComunicacionId{ get; set;}
        [Column("actividad_id")]
        public int ActividadId{ get; set;}
        [Column("trama_enviada")]
        public String TramaEnviada{ get; set;}
        
        [Column("estado_internet")]
        public Boolean EstadoDelInternet{ get; set;}




























































































































    }

}