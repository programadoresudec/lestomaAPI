using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace lestoma.CommonUtils.Helpers
{
    public class Reutilizables
    {
        public const int LONGITUD_CODIGO = 6;
        public static string GenerarCodigoVerificacion()
        {
            string codigo = string.Empty;
            string[] letras = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
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
        public static IEnumerable<string> Split(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }
        public static double TruncateDouble(double value, int decimales)
        {
            double aux_value = Math.Pow(10, decimales);
            return (Math.Truncate(value * aux_value) / aux_value);
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
            //if (BitConverter.IsLittleEndian)
            //    Array.Reverse(Bytes); // We have to reverse
            return BitConverter.ToSingle(Bytes, 0);
        }

        public static float ConvertReceivedTramaToResult(string tramaRecibida)
        {
            List<string> tramaDividida = Split(tramaRecibida, 2).ToList();
            List<byte> valor = new List<byte>();

            for (int i = 0; i < tramaDividida.Count; i++)
            {
                if (i == 4 || i == 5 || i == 6 || i == 7)
                {
                    _ = new byte[1];
                    byte[] byteTemperatura = StringToByteArray(tramaDividida[i]);
                    valor.Add(byteTemperatura.ElementAt(0));
                }
            }
            var result = ByteToIEEEFloatingPoint(valor.ToArray());
            return result;
        }

        public static Byte[] RandomByteDireccionEsclavoAndRegistro()
        {
            using (RNGCryptoServiceProvider rg = new RNGCryptoServiceProvider())
            {
                Byte[] BytesRandom = new Byte[2];
                rg.GetBytes(BytesRandom);
                return BytesRandom;
            }
        }
        public static byte[] IEEEFloatingPointToByte(float floatingPoint)
        {
            byte[] bytes = BitConverter.GetBytes(floatingPoint);
            //if (BitConverter.IsLittleEndian)
            //    Array.Reverse(bytes);
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
        public static string Decrypt(string param)
        {
            return Encryption.EncryptDecrypt.Decrypt(param);
        }
        public static string Encrypt(string param)
        {
            return Encryption.EncryptDecrypt.Encrypt(param);
        }

        public static string GenerateQueryString<T>(T obj)
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var queryStringParams = properties.Select(p => $"{HttpUtility.UrlEncode(p.Name)}={HttpUtility.UrlEncode(p.GetValue(obj)?.ToString() ?? "")}");
            var queryString = $"?{string.Join("&", queryStringParams)}";
            return queryString;
        }
    }
}
