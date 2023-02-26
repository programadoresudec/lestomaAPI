using lestoma.CommonUtils.Core;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.MyException;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace lestoma.Api.Middleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILoggerManager _logger;
        public CustomExceptionMiddleware(RequestDelegate next, IWebHostEnvironment webHostEnvironment, ILoggerManager logger)
        {
            _next = next;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpStatusCodeException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception exceptionObj)
            {
                await HandleExceptionAsync(context, exceptionObj);
            }
        }


        private Task HandleExceptionAsync(HttpContext context, HttpStatusCodeException exception)
        {
            context.Response.ContentType = "application/json";
            string result;
            if (exception is HttpStatusCodeException)
            {
                result = new ResponseDTO()
                {
                    MensajeHttp = exception.Message,
                    IsExito = false,
                    StatusCode = (int)exception.StatusCode
                }.ToString();
                context.Response.StatusCode = (int)exception.StatusCode;
                _logger.LogWarning(result);
            }
            else
            {
                result = new ResponseDTO()
                {
                    MensajeHttp = $"Ha ocurrido un error en la aplicación {_webHostEnvironment.ApplicationName} Error: {exception.Message}",
                    StatusCode = (int)HttpStatusCode.BadRequest
                }.ToString();
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                _logger.LogWarning(result);
            }
            return context.Response.WriteAsync(result);
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            string result = new ResponseDTO()
            {
                MensajeHttp = $"Ha ocurrido un error en la aplicación {_webHostEnvironment.ApplicationName} Error: {exception.Message}",
                StatusCode = (int)HttpStatusCode.InternalServerError
            }.ToString();
            _logger.LogError(result, exception);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(result);
        }
    }
}
