using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models

{

    [Table("detalle_laboratorio", Schema = "laboratorio_lestoma")]

    public class ELaboratorio : ECamposAuditoria
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("componente_laboratorio_id")]
        public Guid ComponenteLaboratorioId { get; set; }
        [Column("tipo_com_id")]
        public int TipoDeComunicacionId { get; set; }

        [Column("valor_componente")]
        public double ValorTramaComponente { get; set; }
        [Column("trama_enviada")]
        public string TramaEnviada { get; set; }
        [Column("estado_internet")]
        public bool EstadoInternet { get; set; }

        public EComponentesLaboratorio ComponenteLaboratorio { get; set; }
        public EProtocoloCOM TipoDeComunicacion { get; set; }
    }
}