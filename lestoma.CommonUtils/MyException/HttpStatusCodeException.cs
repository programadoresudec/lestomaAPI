﻿
using lestoma.CommonUtils.DTOs;
using Newtonsoft.Json.Linq;
using System;
using System.Net;

namespace lestoma.CommonUtils.MyException
{
    public class HttpStatusCodeException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ContentType { get; set; } = @"text/plain";

        public HttpStatusCodeException(HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;
        }

        public HttpStatusCodeException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            this.StatusCode = statusCode;
        }

        public HttpStatusCodeException(ResponseDTO response)
            : base(response.MensajeHttp)
        {
            this.StatusCode = (HttpStatusCode)response.StatusCode;
        }


        public HttpStatusCodeException(HttpStatusCode statusCode, Exception inner)
            : this(statusCode, inner.ToString()) { }

        public HttpStatusCodeException(HttpStatusCode statusCode, JObject errorObject)
            : this(statusCode, errorObject.ToString())
        {
            this.ContentType = @"application/json";
        }
    }
}
