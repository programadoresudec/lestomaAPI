using Newtonsoft.Json;

namespace lestoma.CommonUtils.DTOs
{
    public class LaboratorioComponenteDTO : NameDTO
    {
        public byte DireccionRegistro { get; set; }
        public string Actividad { get; set; }
        [JsonIgnore]
        public string JsonEstado { get; set; }
        public EstadoComponenteDTO EstadoComponente => JsonConvert.DeserializeObject<EstadoComponenteDTO>(this.JsonEstado);
    }

    public class ComponentePorModuloDTO : NameDTO
    {
        public string Actividad { get; set; }

        public byte DireccionRegistro { get; set; }

        public EstadoComponenteDTO EstadoComponente { get; set; }
    }
}
