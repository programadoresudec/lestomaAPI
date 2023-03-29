using lestoma.CommonUtils.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lestoma.CommonUtils.Requests
{
    public class UpaRequest : IId
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "El nombre es requerido.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La descripción es requerida.")]
        [StringLength(5000, MinimumLength = 6, ErrorMessage = "Minimo 10 caracteres y maximo 5000 caracteres.")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "cantidad requerida.")]
        [Range(1, 50, ErrorMessage = "El valor debe ser mayor que 0.")]
        public short CantidadActividades { get; set; }
        [Required(ErrorMessage = "Debe insertar 1 o más protocolos.")]
        public IList<ProtocoloRequest> ProtocolosCOM { get; set; }

    }
    public class UpaEditRequest : IId
    {
        [Required(ErrorMessage = "El id de la upa es requerido.")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "El nombre es requerido.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La descripción es requerida.")]
        [StringLength(5000, MinimumLength = 6, ErrorMessage = "Minimo 10 caracteres y maximo 5000 caracteres.")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "cantidad requerida.")]
        [Range(1, 50, ErrorMessage = "El valor debe ser mayor que 0.")]
        public short CantidadActividades { get; set; }
    }
}
