using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace lestoma.Entidades.Models
{
    public partial class EComponenteLaboratorio
    {
        [NotMapped]
        public EstadoComponente ObjetoJsonEstado =>
            !string.IsNullOrWhiteSpace(this.JsonEstadoComponente) ?
            JsonSerializer.Deserialize<EstadoComponente>(this.JsonEstadoComponente) : null;
    }
    public class EstadoComponente
    {
        public Guid Id { get; set; }
        public string TipoEstado { get; set; }
        public string ByteHexaFuncion { get; set; }
    }
}
