using System;
using System.Collections.Generic;
using System.Text;

namespace lestoma.CommonUtils.Responses
{   public class UserResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int RolId { get; set; }
        public string FullName => $"{Nombre} {Apellido}";
    }
}
