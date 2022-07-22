using System;
using System.Collections.Generic;
using System.Text;

namespace lestoma.CommonUtils.DTOs
{
    public class ArchivoDTO
    {
        public string MIME { get; set; }
        public string FileName { get; set; }
        public byte[] ArchivoBytes { get; set; }
    }
}
