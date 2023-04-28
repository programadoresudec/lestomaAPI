using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Interfaces;
using lestoma.CommonUtils.MyException;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace lestoma.CommonUtils.Requests
{
    public class CreateComponenteRequest
    {
        [Required(ErrorMessage = "El nombre es requerido.")]
        public string Nombre { get; set; }
        public EstadoComponenteDTO TipoEstadoComponente { get; set; }
        public string JsonEstadoComponente => ConvertirJson();
        [Required(ErrorMessage = "El id de la actividad es requerida.")]
        public Guid ActividadId { get; set; }
        [Required(ErrorMessage = "El id de la upa es requerida.")]
        public Guid UpaId { get; set; }
        [Required(ErrorMessage = "El id del modulo es requerido.")]
        public Guid ModuloComponenteId { get; set; }
        [Required(ErrorMessage = "la dirección de registro del componente es requerido.")]
        public byte DireccionRegistro { get; set; }

        public string ConvertirJson()
        {
            if (TipoEstadoComponente == null)
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "el estado del componente es requerido.");

            if (this.TipoEstadoComponente.Id == Guid.Empty)
            {
                this.TipoEstadoComponente.Id = Guid.NewGuid();
            }
            return JsonConvert.SerializeObject(TipoEstadoComponente, formatting: Formatting.Indented);
        }
    }

    public class EditComponenteRequest : IId
    {
        [Required(ErrorMessage = "El id del componente es requerido")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        public EstadoComponenteDTO TipoEstadoComponente { get; set; }
        public string JsonEstadoComponente => ConvertirJson();
        [Required(ErrorMessage = "El id de la actividad es requerida")]
        public Guid ActividadId { get; set; }
        public Guid UpaId { get; set; }
        [Required(ErrorMessage = "la dirección de registro del componente es requerido.")]
        public byte DireccionRegistro { get; set; }
        public Guid ModuloComponenteId { get; set; }
        public string ConvertirJson()
        {
            if (TipoEstadoComponente != null)
            {
                if (this.TipoEstadoComponente.Id == Guid.Empty)
                {
                    this.TipoEstadoComponente.Id = Guid.NewGuid();
                }
                return JsonConvert.SerializeObject(TipoEstadoComponente);
            }
            return string.Empty;
        }
    }

    public class EditComponentAdminRequest : IId
    {
        [Required(ErrorMessage = "El id del componente es requerido")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
    }
}
