using lestoma.CommonUtils.DTOs;
using lestoma.CRC;
using System.Collections.Generic;
using System.Net;

namespace lestoma.CommonUtils.Helpers
{
    public interface ICRCHelper
    {
        public byte[] CalculateCrc16Modbus(string Hexadecimal);
        List<byte> TramaConCRC16Modbus(List<byte> tramaOchoBytes);
        ResponseDTO VerifyCRCOfReceivedTrama(string tramaRecibida);
    }
    public class CRCHelper : ICRCHelper
    {
        public byte[] CalculateCrc16Modbus(string Hexadecimal)
        {
            byte[] bytes = Reutilizables.StringToByteArray(Hexadecimal);

            CrcStdParams.StandartParameters.TryGetValue(CrcAlgorithms.Crc16Modbus, out Parameters crc_p);
            Crc crc = new Crc(crc_p);
            crc.Initialize();

            var crc_bytes = crc.ComputeHash(bytes, 0, bytes.Length);
            return crc_bytes;
        }
        public ResponseDTO VerifyCRCOfReceivedTrama(string tramaRecibida)
        {
            string primerosOchoBytes = tramaRecibida.Substring(0, 16);
            string crcCurrent = tramaRecibida.Substring(tramaRecibida.Length - 4, 4);
            var crcResult = CalculateCrc16Modbus(primerosOchoBytes);
            var hexaCrcResult = Reutilizables.ByteArrayToHexString(new byte[] { crcResult[1], crcResult[0] });
            if (!hexaCrcResult.Equals(crcCurrent))
            {
                return new ResponseDTO
                {
                    IsExito = false,
                    MensajeHttp = "Error en la trama.",
                    StatusCode = (int)HttpStatusCode.Conflict
                };
            }
            return Responses.SetOkResponse();
        }
        public List<byte> TramaConCRC16Modbus(List<byte> tramaOchoBytes)
        {
            var trama = Reutilizables.ByteArrayToHexString(tramaOchoBytes.ToArray());
            var crc = CalculateCrc16Modbus(trama);
            tramaOchoBytes.Add(crc[1]);
            tramaOchoBytes.Add(crc[0]);
            return tramaOchoBytes;
        }
    }
}
