using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.Listados;
using System;
using System.Collections.Generic;
using System.Linq;

namespace lestoma.Pruebas
{
    class Program
    {
        static void Main(string[] args)
        {
            ListadoEstadoComponente listado = new();
            foreach (var item in listado.Listado)
            {
                Console.WriteLine(item.TipoEstado);
            }

            var random = Reutilizables.RandomByteDireccionEsclavoAndRegistro();

            Guid componente = Guid.NewGuid();



            var tercerByte = Reutilizables.ByteArrayToHexString(new byte[] { 15 });
            var primerByte = Reutilizables.StringToByteArray("49 F0 ");

            List<byte> byteArray = new List<byte>() { 67, 204, 128, 0, 0, 0, 0, 0 };

            var bytesFlotante = Reutilizables.IEEEFloatingPointToByte(12.5f);

            byteArray[4] = bytesFlotante[0];
            byteArray[5] = bytesFlotante[1];
            byteArray[6] = bytesFlotante[2];
            byteArray[7] = bytesFlotante[3];

            var trama = Reutilizables.ByteArrayToHexString(byteArray.ToArray());

            var crc = new CRCHelper().CalculateCrc16Modbus(trama);


            byteArray.Add(crc[1]);
            byteArray.Add(crc[0]);

            string tramaCompleta = Reutilizables.ByteArrayToHexString(byteArray.ToArray());

            byte[] bytesTramaCompleta = Reutilizables.StringToByteArray(tramaCompleta);

            for (int i = 0; i < bytesTramaCompleta.Length; i++)
            {
                Console.WriteLine($"byte [{i}] {bytesTramaCompleta[i]}");
            }
            Console.WriteLine($"TRAMA COMPLETA: {tramaCompleta} CRC: {tramaCompleta[16]}{tramaCompleta[17]}" +
                $"{tramaCompleta[18]}{tramaCompleta[19]}");
        }
    }
}
