using System;
using System.Linq;

namespace lestoma.CRC.Helpers
{
    public class HexaToByteHelper
    {
        public static byte[] StringToByteArray(string hex)
        {
            hex = hex.Replace(" ", "");
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

    }
}
