using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lestoma.CommonUtils.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace lestoma.CommonUtils.Requests
{
    public class ModuloRequest :IId
    {
       [Required]
        public string Nombre { get; set; }  
    }
}