using lestoma.CommonUtils.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace lestoma.CommonUtils.Requests.Filters
{
    public class ComponentFilterRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public Guid UpaId { get; set; }
        public Guid ModuloId { get; set; }
    }
}
