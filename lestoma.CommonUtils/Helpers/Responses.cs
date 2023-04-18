using lestoma.CommonUtils.DTOs;
using System;
using System.Net;

namespace lestoma.CommonUtils.Helpers
{
    public class Responses
    {
        public static ResponseDTO SetOkResponse(Object pData = null, string Mensaje = "Ok")
        {
            ResponseDTO pResponse = new ResponseDTO();
            pResponse.StatusCode = (int)HttpStatusCode.OK;
            pResponse.MensajeHttp = Mensaje;
            if (pData != null && !String.IsNullOrEmpty(pData.ToString()))
            {
                pResponse.Data = pData;
            }
            pResponse.IsExito = true;
            return pResponse;
        }

        public static ResponseDTO SetCreatedResponse(Object pData = null, string Mensaje = "Se ha creado satisfactoriamente.")
        {
            ResponseDTO pResponse = new ResponseDTO();
            pResponse.StatusCode = (int)HttpStatusCode.Accepted;
            pResponse.MensajeHttp = Mensaje;
            if (pData != null && !String.IsNullOrEmpty(pData.ToString()))
            {
                pResponse.Data = pData;
            }
            pResponse.IsExito = true;
            return pResponse;
        }

        public static ResponseDTO SetLLaveFkUsedResponse(string Mensaje)
        {
            ResponseDTO pResponse = new ResponseDTO();
            pResponse.StatusCode = (int)HttpStatusCode.NotImplemented;
            pResponse.MensajeHttp = Mensaje;
            pResponse.IsExito = false;
            return pResponse;
        }

        public static ResponseDTO SetAcceptedResponse(Object pData = null, string mensaje = "Se ha aceptado satisfactoriamente.")
        {
            ResponseDTO pResponse = new ResponseDTO();
            pResponse.StatusCode = (int)HttpStatusCode.Accepted;
            pResponse.MensajeHttp = mensaje;
            if (pData != null && !String.IsNullOrEmpty(pData.ToString()))
            {
                pResponse.Data = pData;
            }
            pResponse.IsExito = true;
            return pResponse;
        }

        public static ResponseDTO SetOkMessageEditResponse(Object pData = null)
        {
            ResponseDTO pResponse = new ResponseDTO();
            pResponse.StatusCode = (int)HttpStatusCode.OK;
            pResponse.MensajeHttp = "Se ha editado satisfactoriamente.";
            if (pData != null && !String.IsNullOrEmpty(pData.ToString()))
            {
                pResponse.Data = pData;
            }
            pResponse.IsExito = true;
            return pResponse;
        }

        public static ResponseDTO SetInternalErrorResponse(Exception exception, string Error)
        {
            ResponseDTO pResponse = new ResponseDTO();
            pResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
            pResponse.MensajeHttp = $"[Error] {exception.Message}";
            pResponse.IsExito = false;
            return pResponse;
        }
    }
}
