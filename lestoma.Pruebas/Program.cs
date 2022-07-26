using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.Interfaces;
using lestoma.CommonUtils.Listados;
using lestoma.CRC.Helpers;
using System;
using System.Collections.Generic;

namespace lestoma.Pruebas
{
    class Program
    {
        static void Main(string[] args)
        {
            ListadoEstadoComponente listado = new ListadoEstadoComponente();
            foreach (var item in listado.Listado)
            {
                Console.WriteLine(item.TipoEstado);
            }

            List<byte> byteArray = new List<byte>() { 111, 1, 240, 0, 0, 0, 0, 0 };


            var bytesFlotante = Reutilizables.IEEEFloatingPointToByte(0);

            byteArray[4] = bytesFlotante[0];
            byteArray[5] = bytesFlotante[1];
            byteArray[6] = bytesFlotante[2];
            byteArray[7] = bytesFlotante[3];

            var trama = Reutilizables.ByteArrayToHexString(byteArray.ToArray());

            var crc = CalcularCRCHelper.CalculateCrc16Modbus(trama);


            byteArray.Add(crc[1]);
            byteArray.Add(crc[0]);

            string tramaCompleta = Reutilizables.ByteArrayToHexString(byteArray.ToArray());

            Console.WriteLine($"TRAMA COMPLETA: {tramaCompleta} CRC: {tramaCompleta[16]}{tramaCompleta[17]}" +
                $"{tramaCompleta[18]}{tramaCompleta[19]}");
        }


    }
}
