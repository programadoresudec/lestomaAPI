using lestoma.CommonUtils.DTOs;
using System;
using System.Net;

namespace lestoma.CommonUtils.Helpers
{
    public class Responses
    {
        public static ResponseDTO SetOkResponse(Object pData = null, string Mensaje = "Ok")
        {
            ResponseDTO pResponse = new ResponseDTO
            {
                StatusCode = (int)HttpStatusCode.OK,
                MensajeHttp = Mensaje
            };
            if (pData != null && !String.IsNullOrEmpty(pData.ToString()))
            {
                pResponse.Data = pData;
            }
            pResponse.IsExito = true;
            return pResponse;
        }

        public static ResponseDTO SetCreatedResponse(Object pData = null, string Mensaje = "Se ha creado satisfactoriamente.")
        {
            ResponseDTO pResponse = new ResponseDTO
            {
                StatusCode = (int)HttpStatusCode.Accepted,
                MensajeHttp = Mensaje
            };
            if (pData != null && !String.IsNullOrEmpty(pData.ToString()))
            {
                pResponse.Data = pData;
            }
            pResponse.IsExito = true;
            return pResponse;
        }

        public static ResponseDTO SetLLaveFkUsedResponse(string Mensaje)
        {
            ResponseDTO pResponse = new ResponseDTO
            {
                StatusCode = (int)HttpStatusCode.NotImplemented,
                MensajeHttp = Mensaje,
                IsExito = false
            };
            return pResponse;
        }

        public static ResponseDTO SetAcceptedResponse(Object pData = null, string mensaje = "Se ha aceptado satisfactoriamente.")
        {
            ResponseDTO pResponse = new ResponseDTO
            {
                StatusCode = (int)HttpStatusCode.Accepted,
                MensajeHttp = mensaje
            };
            if (pData != null && !String.IsNullOrEmpty(pData.ToString()))
            {
                pResponse.Data = pData;
            }
            pResponse.IsExito = true;
            return pResponse;
        }

        public static ResponseDTO SetOkMessageEditResponse(Object pData = null)
        {
            ResponseDTO pResponse = new ResponseDTO
            {
                StatusCode = (int)HttpStatusCode.OK,
                MensajeHttp = "Se ha editado satisfactoriamente."
            };
            if (pData != null && !String.IsNullOrEmpty(pData.ToString()))
            {
                pResponse.Data = pData;
            }
            pResponse.IsExito = true;
            return pResponse;
        }

        public static ResponseDTO SetInternalErrorResponse(Exception exception, string Error)
        {
            ResponseDTO pResponse = new ResponseDTO
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                MensajeHttp = $"[Error] {exception.Message}",
                IsExito = false
            };
            return pResponse;
        }
    }
}
