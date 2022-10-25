using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace lestoma.Entidades.Models
{
    public partial class EComponenteLaboratorio
    {
        [NotMapped]
        public EstadosComponentes ObjetoJsonEstado => JsonSerializer.Deserialize<EstadosComponentes>(this.JsonEstadoComponente);
    }
    public class EstadosComponentes
    {
        public Guid Id { get; set; }
        public string TipoEstado { get; set; }
        public string ByteFuncion { get; set; }
    }
}
