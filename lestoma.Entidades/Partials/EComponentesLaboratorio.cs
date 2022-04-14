using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models
{
    public partial class EComponentesLaboratorio
    {
        [NotMapped]
        public EstadosComponentes Descripcion => JsonConvert.DeserializeObject<EstadosComponentes>(this.TiposEstadoComponente);
    }
    public class EstadosComponentes
    {
        public string Id { get; set; }
        public string TipoEstado { get; set; }
        public string TercerByteTrama { get; set; }
        public string CuartoByteTrama { get; set; }
    }
}
