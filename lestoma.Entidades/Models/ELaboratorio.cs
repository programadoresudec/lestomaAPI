using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models

{
    [Table("detalle_laboratorio", Schema = "laboratorio_lestoma")]
    public partial class ELaboratorio : ECamposAuditoria
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("componente_laboratorio_id")]
        public Guid ComponenteLaboratorioId { get; set; }
        [Column("tipo_com_id")]
        public int TipoDeComunicacionId { get; set; }
        [Column("set_point")]
        public double? SetPoint { get; set; }
        [Column("trama_enviada")]
        public string TramaEnviada { get; set; }
        [Column("estado_internet")]
        public bool EstadoInternet { get; set; }
        [Column("fecha_creacion_dispositivo")]
        public DateTime FechaCreacionDispositivo { get; set; }
        public EComponenteLaboratorio ComponenteLaboratorio { get; set; }
        public EProtocoloCOM TipoDeComunicacion { get; set; }
    }
}