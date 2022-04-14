﻿using lestoma.CommonUtils.Interfaces;
using lestoma.CommonUtils.Listados;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace lestoma.CommonUtils.Requests
{
    public class CrearComponenteRequest : IId
    {
        [Required]
        public string Nombre { get; set; }
        public Guid Id { get; set; }
        public EstadosComponentesDTO componentes { get; set; }
        public string TiposEstadoComponente => ConvertirJson();
        public Guid ActividadId { get; set; }
        public int ModuloComponenteId { get; set; }

        public string ConvertirJson()
        {
            this.componentes.Id = Guid.NewGuid().ToString();
            return JsonConvert.SerializeObject(this.componentes);
        }
    }
}
