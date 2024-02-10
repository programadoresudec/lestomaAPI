using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace lestoma.CommonUtils.Requests.Filters
{
    public class UpaActivitiesFilterRequest
    {
        [Required(ErrorMessage = "Requerida una actividad")]
        public IEnumerable<Guid> ActividadesId { get; set; }
        [Required(ErrorMessage = "Requerida la upa")]
        public Guid UpaId { get; set; }
        public Guid ModuloId { get; set; }
    }
}
