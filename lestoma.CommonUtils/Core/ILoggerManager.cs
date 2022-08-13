using System;

namespace lestoma.CommonUtils.Core
{
    public interface ILoggerManager
    {
        //Metodo Genera Mensaje Tipo Informativo 
        //con parametro un mensaje
        void LogInformation(string message);

        //Metodo Genera Mensaje Tipo Informativo 
        //con parametro un mensaje y objeto Excepcion
        void LogInformation(string message, Exception ex);

        //Metodo Genera Mensaje Tipo Advertencia 
        //con parametro un mensaje
        void LogWarning(string message);

        //Metodo Genera Mensaje Tipo Advertencia 
        //con parametro un mensaje y objeto Excepcion
        void LogWarning(string message, Exception ex);

        //Metodo Genera Mensaje Tipo Error 
        //con parametro un mensaje
        void LogError(string message);

        //Metodo Genera Mensaje Tipo Error 
        //con parametro un mensaje y objeto Excepcion
        void LogError(string message, Exception ex);
    }
}
