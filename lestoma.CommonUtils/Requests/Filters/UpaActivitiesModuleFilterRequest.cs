using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lestoma.CommonUtils.Requests.Filters
{
    public class UpaActivitiesModuleFilterRequest
    {
        [Required(ErrorMessage = "Requerida una actividad")]
        public IEnumerable<Guid> ActividadesId { get; set; }
        [Required(ErrorMessage = "Requerida la upa")]
        public Guid UpaId { get; set; }
        [Required(ErrorMessage = "Requerido el modulo")]
        public Guid ModuloId { get; set; }
    }
}
