using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models

{
    [Table("detalle_laboratorio", Schema = "laboratorio_lestoma")]
    public partial class ELaboratorio
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("componente_laboratorio_id")]
        [Required(ErrorMessage = "Campo requerido.")]
        public Guid ComponenteLaboratorioId { get; set; }
        [Column("dato_trama_enviada")]
        public double? ValorCalculadoTramaEnviada { get; set; }
        [Column("dato_trama_recibida")]
        public double? ValorCalculadoTramaRecibida { get; set; }
        [Column("trama_enviada")]
        public string TramaEnviada { get; set; }
        [Column("trama_recibida")]
        public string TramaRecibida { get; set; }
        [Column("estado_internet")]
        public bool EstadoInternet { get; set; }
        [Column("ip")]
        [Required(ErrorMessage = "Campo requerido.")]
        public string Ip { get; set; }
        [Column("session")]
        [Required(ErrorMessage = "Campo requerido.")]
        public string Session { get; set; }
        [Column("tipo_de_aplicacion")]
        [Required(ErrorMessage = "Campo requerido.")]
        public string TipoDeAplicacion { get; set; }
        [Column("fecha_creacion_server")]
        [Required(ErrorMessage = "Campo requerido.")]
        public DateTime FechaCreacionServer { get; set; }
        [Column("fecha_creacion_dispositivo")]
        [Required(ErrorMessage = "Campo requerido.")]
        public DateTime FechaCreacionDispositivo { get; set; }
        public EComponenteLaboratorio ComponenteLaboratorio { get; set; }
    }
}