using lestoma.CRC;

namespace lestoma.CommonUtils.Helpers
{
    public interface ICRCHelper
    {
        public byte[] CalculateCrc16Modbus(string Hexadecimal);
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
    }
}
