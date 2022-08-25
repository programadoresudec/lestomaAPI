using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Interfaces;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace lestoma.CommonUtils.Requests
{
    public class CreateOrEditComponenteRequest : IId
    {
        [Required]
        public string Nombre { get; set; }
        public Guid Id { get; set; }
        public EstadosComponentesDTO TipoEstadoComponente { get; set; }
        public string JsonEstadoComponente => ConvertirJson();
        [Required]
        public Guid ActividadId { get; set; }
        [Required]
        public Guid UpaId { get; set; }
        [Required]
        public int ModuloComponenteId { get; set; }

        public string ConvertirJson()
        {
            this.TipoEstadoComponente.Id = Guid.NewGuid();
            return JsonConvert.SerializeObject(TipoEstadoComponente);
        }
    }
}
