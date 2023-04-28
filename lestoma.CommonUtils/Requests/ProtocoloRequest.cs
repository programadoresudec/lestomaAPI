using lestoma.CommonUtils.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace lestoma.CommonUtils.Requests
{
    public class ProtocoloRequest
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo requerido.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Campo requerido.")]
        public byte PrimerByteTrama { get; set; }
        [Required(ErrorMessage = "Campo requerido.")]
        [StringLength(5, MinimumLength = 2, ErrorMessage = "Minimo 2 caracteres y maximo 5 caracteres")]
        public string Sigla { get; set; }
        public Guid UpaId { get; set; }
        public string ByteHexa => Reutilizables.ByteArrayToHexString(new byte[] { PrimerByteTrama });
    }
}
