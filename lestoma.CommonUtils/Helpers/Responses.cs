using lestoma.CommonUtils.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

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

        public static ResponseDTO SetCreatedResponse(Object pData = null)
        {
            ResponseDTO pResponse = new ResponseDTO();
            pResponse.StatusCode = (int)HttpStatusCode.Accepted;
            pResponse.MensajeHttp = "Se ha creado satisfactoriamente.";
            if (pData != null && !String.IsNullOrEmpty(pData.ToString()))
            {
                pResponse.Data = pData;
            }
            pResponse.IsExito = true;
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
    }
}
