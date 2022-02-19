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
        }
    }
}
