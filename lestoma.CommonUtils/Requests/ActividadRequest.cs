using lestoma.CommonUtils.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace lestoma.CommonUtils.Requests
{
    public class ActividadRequest : IId
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
