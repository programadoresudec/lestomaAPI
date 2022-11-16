using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Requests;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace lestoma.Entidades.Models
{
    public partial class EUpaActividad
    {
        [NotMapped]
        public IEnumerable<ActividadRequest> Actividades { get; set; }
        [NotMapped]
        public UserDTO User { get; set; }
        [NotMapped]
        public short ActividadesAsignadas { get; set; }
    }
}
