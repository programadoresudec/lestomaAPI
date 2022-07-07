using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace lestoma.Entidades.Models
{
    public partial class EComponenteLaboratorio
    {
        [NotMapped]
        public EstadosComponentes Descripcion => JsonSerializer.Deserialize<EstadosComponentes>(this.JsonEstadoComponente);
    }
    public class EstadosComponentes
    {
        public string Id { get; set; }
        public string TipoEstado { get; set; }
        public string TercerByteTrama { get; set; }
        public string CuartoByteTrama { get; set; }
    }
}
