using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lestoma.CommonUtils.Requests.Filters
{
    public class UpaActivitiesModuleFilterRequest : UpaActivitiesFilterRequest
    {
        [Required(ErrorMessage = "Requerido un modulo")]
        public Guid ModuloId { get; set; }
    }
}
