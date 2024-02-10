using System;

namespace lestoma.CommonUtils.Requests.Filters
{
    public class UpaModuleActivityFilterRequest
    {
        public Guid UpaId { get; set; }
        public Guid ModuloId { get; set; }
        public Guid ActividadId { get; set; }
    }
}