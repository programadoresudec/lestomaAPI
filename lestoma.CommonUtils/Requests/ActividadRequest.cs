using lestoma.CommonUtils.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace lestoma.CommonUtils.Requests
{
    public class ActividadRequest : IId
    {
        [Required]
        public string Nombre { get; set; }
        public Guid Id { get; set; }
    }
}
