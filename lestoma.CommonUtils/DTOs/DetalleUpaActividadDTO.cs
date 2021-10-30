using lestoma.CommonUtils.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace lestoma.CommonUtils.DTOs
{
    public class DetalleUpaActividadDTO : AuditoriaDTO
    {
        public Guid UpaId { get; set; }
        public int UsuarioId { get; set; }
        public UserDTO User { get; set; } = new UserDTO();
        public UpaRequest Upa { get; set; } = new UpaRequest();
        public List<ActividadRequest> Actividades { get; set; } = new List<ActividadRequest>();
    }
}
