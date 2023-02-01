﻿using Newtonsoft.Json;

namespace lestoma.CommonUtils.DTOs
{
    public class LaboratorioComponenteDTO : NameDTO
    {
        public string Actividad { get; set; }
        [JsonIgnore]
        public string JsonEstado { get; set; }
        public EstadoComponenteDTO EstadoComponente => JsonConvert.DeserializeObject<EstadoComponenteDTO>(this.JsonEstado);
    }
}
