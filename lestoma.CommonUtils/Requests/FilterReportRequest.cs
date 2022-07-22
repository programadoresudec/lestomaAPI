using lestoma.CommonUtils.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lestoma.CommonUtils.Requests
{
    public class FilterReportDailyRequest
    {
        [Range(0, 23, ErrorMessage = "La hora tiene que estar entre {0} - {1}.")]
        public int Hour { get; set; }
        [Range(0, 59, ErrorMessage = "Los minutos tiene que estar entre {0} - {1}.")]
        public int Minute { get; set; }
    }
    public class FilterReportRequest
    {
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public int UpaId { get; set; }
        public GrupoTipoArchivo TipoFormato { get; set; }
    }
    public class FilterDateRequest
    {
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
    }

    public class FilterReportComponentRequest
    {
        public FilterReportRequest Filtro { get; set; }
        public List<Guid> ComponentesId { get; set; }
    }
}
