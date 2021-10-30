using lestoma.CommonUtils.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace lestoma.CommonUtils.DTOs
{
    public class NameDTO : IId
    {
        
        public string Nombre { get; set; }
        public Guid Id { get; set; }
    }
}
