using lestoma.CommonUtils.Requests;
using System;

namespace lestoma.CommonUtils.DTOs
{
    public class BuzonDTO
    {
        public int Id { get; set; }
        public DetalleBuzon Detalle { get; set; } = new DetalleBuzon();
        public UserDTO User { get; set; } = new UserDTO();
        public DateTime FechaCreacion { get; set; }

    }
}
