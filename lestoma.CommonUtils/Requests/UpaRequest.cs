using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lestoma.CommonUtils.Requests
{
    public class UpaRequest : IId
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public short CantidadActividades { get; set; }
        public Guid Id { get; set; }
        [Required]
        public ICollection<ProtocoloDTO> ProtocolosCOM { get; set; }

    }
}
