using System;
using System.Collections.Generic;
using System.Text;

namespace lestoma.CommonUtils.DTOs
{
    public class TramaComponenteDTO
    {
        public string TramaOutPut { get; set; }
        public string TramaInPut { get; set; }
        public double? SetPointIn { get; set; }
        public double? SetPointOut { get; set; }
    }
}
