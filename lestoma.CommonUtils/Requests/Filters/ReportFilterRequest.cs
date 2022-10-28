using lestoma.CommonUtils.Core.Attributes;
using lestoma.CommonUtils.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lestoma.CommonUtils.Requests.Filters
{
    public class ReportDailyFilterRequest
    {
        [Range(0, 23, ErrorMessage = "La hora tiene que estar entre {1} - {2}.")]
        public int Hour { get; set; }
        [Range(0, 59, ErrorMessage = "Los minutos tiene que estar entre {1} - {2}.")]
        public int Minute { get; set; }
    }
    public class ReportFilterRequest
    {
        [Required(ErrorMessage = "Fecha Inicial requerida.")]
        [FromNow]
        public DateTime FechaInicial { get; set; }
        [Required(ErrorMessage = "Fecha Final requerida.")]
        [FromNow]
        public DateTime FechaFinal { get; set; }
        public Guid UpaId { get; set; }
        [Required(ErrorMessage = "El tipo de formato es requerido.")]
        [EnumValidateExists(typeof(GrupoTipoArchivo))]
        public GrupoTipoArchivo TipoFormato { get; set; }
    }
    public class DateFilterRequest
    {
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
    }

    public class ReportComponentFilterRequest
    {
        public ReportFilterRequest Filtro { get; set; }
        public List<Guid> ComponentesId { get; set; }
    }
}
