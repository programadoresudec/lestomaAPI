using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models
{
    public class ECamposAuditoria
    {
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
        [Column("fecha_actualizacion_server")]
        public DateTime? FechaActualizacionServer { get; set; }
    }
}