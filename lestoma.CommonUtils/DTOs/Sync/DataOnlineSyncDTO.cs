using System;
using System.Collections.Generic;

namespace lestoma.CommonUtils.DTOs.Sync
{
    public class DataOnlineSyncDTO
    {
        public Guid Id { get; set; }
        public NameDTO Modulo { get; set; }
        public NameDTO Actividad { get; set; }
        public NameDTO Upa { get; set; }
        public string NombreComponente { get; set; }
        public string DescripcionEstadoJson { get; set; }
        public byte DireccionRegistro { get; set; }
        public List<ProtocoloSyncDTO> Protocolos { get; set; }
    }
    public class ProtocoloSyncDTO
    {
        public string Nombre { get; set; }
        public byte PrimerByteTrama { get; set; }
    }
}
