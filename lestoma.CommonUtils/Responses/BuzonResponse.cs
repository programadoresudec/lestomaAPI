using lestoma.CommonUtils.Requests;
using System;
using System.Collections.Generic;

namespace lestoma.CommonUtils.Responses
{
    public class BuzonResponse
    {
        public int Id { get; set; }
        public DetalleBuzon Detalle { get; set; } = new DetalleBuzon();
        public UserResponse User { get; set; } = new UserResponse();
        public DateTime FechaCreacion { get; set; }

    }
}
