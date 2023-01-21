using lestoma.CommonUtils.Helpers;
using System;

namespace lestoma.CommonUtils.DTOs
{
    public class EstadoComponenteDTO
    {
        public Guid Id { get; set; }
        public string TipoEstado { get; set; }
        public string ByteHexaFuncion { get; set; }
        public byte ByteDecimalFuncion => !string.IsNullOrWhiteSpace(this.ByteHexaFuncion) ? 
            Reutilizables.StringToByteArray(this.ByteHexaFuncion)[0] : Byte.MinValue;
    }
}
