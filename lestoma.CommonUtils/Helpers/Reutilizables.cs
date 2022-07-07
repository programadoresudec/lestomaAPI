using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace lestoma.CommonUtils.Helpers
{
    public class Reutilizables
    {
        public const int LONGITUD_CODIGO = 6;
        public static string generarCodigoVerificacion()
        {
            string codigo = string.Empty;
            string[] letras = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
                                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
            Random EleccionAleatoria = new Random();

            for (int i = 0; i < LONGITUD_CODIGO; i++)
            {
                int LetraAleatoria = EleccionAleatoria.Next(0, 100);
                int NumeroAleatorio = EleccionAleatoria.Next(0, 9);

                if (LetraAleatoria < letras.Length)
                {
                    codigo += letras[LetraAleatoria];
                }
                else
                {
                    codigo += NumeroAleatorio.ToString();
                }

            }
            return codigo;
        }
        public static byte[] StringToByteArray(string hex)
        {
            hex = hex.Replace(" ", "");
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
        public static string ByteArrayToHexString(byte[] Bytes)
        {
            StringBuilder Result = new StringBuilder(Bytes.Length * 2);
            string HexAlphabet = "0123456789ABCDEF";

            foreach (byte B in Bytes)
            {
                Result.Append(HexAlphabet[(int)(B >> 4)]);
                Result.Append(HexAlphabet[(int)(B & 0xF)]);
            }

            return Result.ToString();
        }

        public static float ByteToIEEEFloatingPoint(byte[] Bytes)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(Bytes); // We have to reverse
            return BitConverter.ToSingle(Bytes, 0);
        }
        public static byte[] IEEEFloatingPointToByte(float floatingPoint)
        {
            byte[] bytes = BitConverter.GetBytes(floatingPoint);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return bytes;
        }
        public T ReadJSON<T>(string filePath)
        {
            try
            {
                var assembly = this.GetType().Assembly;
                var recursos = this.GetType().Assembly.GetManifestResourceNames();
                string rutaCompleta = recursos.SingleOrDefault(x => x.Contains(filePath));
                T data;
                using (var stream = assembly.GetManifestResourceStream(rutaCompleta))
                {
                    using (var reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        string json = reader.ReadToEnd();
                        data = JsonConvert.DeserializeObject<T>(json);
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return default;
            }
        }
    }
}
