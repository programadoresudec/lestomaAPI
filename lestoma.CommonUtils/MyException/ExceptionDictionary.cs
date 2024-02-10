using System.Collections.Generic;

namespace lestoma.CommonUtils.MyException
{
    public class ExceptionDictionary
    {
        private readonly Dictionary<int, string> ExceptionCodes = new Dictionary<int, string>()
        {
            { -2146233088, "Error de lectura El dispositivo Bluetooth no está habilitado.\n\n Es posible que el socket esté cerrado o haya expirado el tiempo de espera." },
            { -2147467261, "El servidor remoto devolvió un error: (500) Error interno del servidor." },
            { -2146233079, "Referencia a objeto no establecida como instancia de un objeto." },
            { -2147467262, "Se produjo un error inesperado." },
            { -2146233087, "Excepción de argumento no válido." },
            { -2147467263, "No se encontró ningún dispositivo Bluetooth disponible." },
            { -2146233090, "Error al conectar con el dispositivo Bluetooth." },
            { -2146233091, "Error al cerrar la conexión Bluetooth." },
            { -2146233092, "Tiempo de espera agotado al conectar con el dispositivo Bluetooth." },
            { -2146233093, "Error al enviar datos a través de la conexión Bluetooth." },
            { -2146233094, "Error al recibir datos a través de la conexión Bluetooth." },
            { -2146233095, "El dispositivo Bluetooth remoto ha cerrado la conexión." },
        };

        public (string MessageError, bool IsExceptionCode) GetExceptionDescription(int exceptionCode)
        {
            if (ExceptionCodes.TryGetValue(exceptionCode, out string description))
            {
                return (description, true);
            }

            return ("Código de excepción no identificado.", false);
        }
    }
}
