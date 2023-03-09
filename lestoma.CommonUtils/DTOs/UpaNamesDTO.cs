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
    public class NameProtocoloDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public byte PrimerByteTrama { get; set; }
    }
}
