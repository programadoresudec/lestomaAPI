using Newtonsoft.Json;
using System;

namespace lestoma.CommonUtils.DTOs
{
    public class ListadoComponenteDTO : AuditoriaDTO
    {
        public Guid Id { get; set; }
        public string Modulo { get; set; }
        public string Actividad { get; set; }
        public string Upa { get; set; }
        public string Nombre { get; set; }
        public byte DireccionRegistro { get; set; }
        public bool IsVisible { get; set; }
        [JsonIgnore]
        public string JsonEstadoComponente { get; set; }
        public EstadoComponenteDTO TipoEstadoComponente => JsonConvert.DeserializeObject<EstadoComponenteDTO>(this.JsonEstadoComponente);

    }

    public class ComponenteDTO : AuditoriaDTO
    {
        public Guid Id { get; set; }
        public string Modulo { get; set; }
        public string Actividad { get; set; }
        public string Upa { get; set; }
        public string Nombre { get; set; }
        public byte DireccionRegistro { get; set; }
        public bool IsVisible { get; set; }
        public EstadoComponenteDTO TipoEstadoComponente { get; set; }
    }
}
