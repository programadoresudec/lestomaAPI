using lestoma.CommonUtils.Interfaces;
using lestoma.CommonUtils.Listados;
using System;

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
            string i = Guid.NewGuid().ToString();
            Console.WriteLine(i);


            byte[] byteArray = { 73, 111, 240, 0, 0, 0, 0, 0, 15};

            string hexString = BitConverter.ToString(byteArray);

            Console.WriteLine(hexString);
            Console.WriteLine(hexString.Replace('-', ' '));
        }


    }
}
