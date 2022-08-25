using lestoma.CommonUtils.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace lestoma.CommonUtils.Requests.Filters
{
    public class ComponentFilterRequest
    {
        public Paginacion Paginacion { get; set; } = new Paginacion();
        public Guid UpaId { get; set; }
    }
}
