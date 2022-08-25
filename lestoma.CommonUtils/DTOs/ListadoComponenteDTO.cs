using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace lestoma.CommonUtils.DTOs
{
    public class ListadoComponenteDTO: AuditoriaDTO
    {
        public Guid Id { get; set; }
        public string Modulo { get; set; }
        public string Actividad { get; set; }
        public string Upa { get; set; }
        public string Nombre { get; set; }
        [JsonIgnore]
        public string JsonEstadoComponente { get; set; }
        public EstadosComponentesDTO TipoEstadoComponente => JsonConvert.DeserializeObject<EstadosComponentesDTO>(this.JsonEstadoComponente);

    }
}
