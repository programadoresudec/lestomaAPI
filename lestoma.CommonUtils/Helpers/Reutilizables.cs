using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace lestoma.CommonUtils.Helpers
{
    public static class Reutilizables
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
    }
}
