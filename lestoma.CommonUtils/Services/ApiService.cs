using lestoma.CommonUtils.Interfaces;
using lestoma.CommonUtils.Responses;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace lestoma.CommonUtils.Services
{
    public class ApiService : IApiService
    {
        public HttpResponseMessage ResponseMessage { get; set; }
        public Response Respuesta { get; set; }

        #region GetList Api service with token
        public async Task<Response> GetListAsyncWithToken<T>(string urlBase, string controller, string token, bool isLogin)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase),
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                ResponseMessage = await client.GetAsync(controller);
                string jsonString = await ResponseMessage.Content.ReadAsStringAsync();
                if (!ResponseMessage.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsExito = false,
                        Mensaje = mostrarMensajePersonalizadoStatus(ResponseMessage.StatusCode, Respuesta.Mensaje)
                    };
                }
                T item = JsonConvert.DeserializeObject<T>(jsonString);

                return new Response
                {
                    IsExito = true,
                    Data = item
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsExito = false,
                    Mensaje = ex.Message
                };
            }
        }

        #endregion

        #region Post Api service
        public async Task<Response> PostAsync<T>(string urlBase, string controller, T model)
        {
            try
            {
                string json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase),
                };

                ResponseMessage = await client.PostAsync(controller, content);
                string jsonString = await ResponseMessage.Content.ReadAsStringAsync();
                Respuesta = JsonConvert.DeserializeObject<Response>(jsonString);
                if (!ResponseMessage.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsExito = false,
                        Mensaje = mostrarMensajePersonalizadoStatus(ResponseMessage.StatusCode, Respuesta.Mensaje)
                    };
                }

                return Respuesta;
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsExito = false,
                    Mensaje = ResponseMessage != null ? mostrarMensajePersonalizadoStatus(ResponseMessage.StatusCode, string.Empty) : ex.Message
                };
            }
        }
        #endregion

        #region Post Api service with token
        public async Task<Response> PostAsyncWithToken<T>(string urlBase, string controller, T model, string token, bool isLogin)
        {
            try
            {

                string json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase),
                };
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                ResponseMessage = await client.PostAsync(controller, content);
                string jsonString = await ResponseMessage.Content.ReadAsStringAsync();
                Respuesta = JsonConvert.DeserializeObject<Response>(jsonString);
                if (!ResponseMessage.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsExito = false,
                        Mensaje = mostrarMensajePersonalizadoStatus(ResponseMessage.StatusCode, Respuesta.Mensaje)
                    };
                }

                return Respuesta;
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsExito = false,
                    Mensaje = ResponseMessage != null ? mostrarMensajePersonalizadoStatus(ResponseMessage.StatusCode, string.Empty) : ex.Message
                };
            }
        }
        #endregion

        #region mostrar Mensaje Personalizado para la solicitud del service

        private string mostrarMensajePersonalizadoStatus(System.Net.HttpStatusCode statusCode, string mensajeDeLaApi)
        {
            string mensaje = string.Empty;
            if (!string.IsNullOrWhiteSpace(mensajeDeLaApi))
            {
                mensaje = mensajeDeLaApi;
            }
            else
            {
                switch (statusCode)
                {
                    case System.Net.HttpStatusCode.Accepted:
                        mensaje = "La solicitud fue aceptada";
                        break;
                    case System.Net.HttpStatusCode.Ambiguous:
                        mensaje = "url ambiguo";
                        break;
                    case System.Net.HttpStatusCode.BadGateway:
                        mensaje = "respuesta no valida";
                        break;
                    case System.Net.HttpStatusCode.BadRequest:
                        mensaje = "La solicitud malformada";
                        break;
                    case System.Net.HttpStatusCode.Conflict:
                        mensaje = "conflicto con el estado actual del server";
                        break;
                    case System.Net.HttpStatusCode.Continue:
                        mensaje = "todo va bien por ahora continua";
                        break;
                    case System.Net.HttpStatusCode.Created:
                        mensaje = "Solicitud con exito y se creo un recurso";
                        break;
                    case System.Net.HttpStatusCode.ExpectationFailed:
                        mensaje = "el expect solicitada no puede ser cumplida";
                        break;
                    case System.Net.HttpStatusCode.Forbidden:
                        mensaje = "no posees los permisos necesarios";
                        break;
                    case System.Net.HttpStatusCode.Found:
                        mensaje = "url cambiado temporalmente";
                        break;
                    case System.Net.HttpStatusCode.GatewayTimeout:
                        mensaje = " tiempo de respuesta null ";
                        break;
                    case System.Net.HttpStatusCode.Gone:
                        mensaje = "contenido borrado  del server";
                        break;
                    case System.Net.HttpStatusCode.HttpVersionNotSupported:
                        mensaje = "el servidor no soporta la version http";
                        break;
                    case System.Net.HttpStatusCode.InternalServerError:
                        mensaje = "error interno del server";
                        break;
                    case System.Net.HttpStatusCode.LengthRequired:
                        mensaje = "rechazo del server por cabecera inadecuada";
                        break;
                    case System.Net.HttpStatusCode.MethodNotAllowed:
                        mensaje = "metodo de solicitud no soportado";
                        break;
                    case System.Net.HttpStatusCode.Moved:
                        mensaje = "peticiones movidas a la url dada";
                        break;
                    case System.Net.HttpStatusCode.NoContent:
                        mensaje = "su peticion no tiene ningun contenido";
                        break;
                    case System.Net.HttpStatusCode.NonAuthoritativeInformation:
                        mensaje = "peticion obtenida de otro server al solicitado";
                        break;
                    case System.Net.HttpStatusCode.NotAcceptable:
                        mensaje = "el servidor no puede responder los datos en ningun valor aceptado";
                        break;
                    case System.Net.HttpStatusCode.NotFound:
                        mensaje = "Petición no encontrada.";
                        break;
                    case System.Net.HttpStatusCode.NotImplemented:
                        mensaje = "el server no soporta alguna funcionalidad";
                        break;
                    case System.Net.HttpStatusCode.NotModified:
                        mensaje = "peticion  o url modificada";
                        break;
                    case System.Net.HttpStatusCode.OK:
                        mensaje = "Solicitud realizada correctamente";
                        break;
                    case System.Net.HttpStatusCode.PartialContent:
                        mensaje = "la peticion serivira parcialmente el contenido solicitado";
                        break;
                    case System.Net.HttpStatusCode.PaymentRequired:
                        mensaje = "este error es ambiguo no esta en uso comuniquese con el ingeniero";
                        break;
                    case System.Net.HttpStatusCode.PreconditionFailed:
                        mensaje = "el server no puede cumplir con alguna condicion impuesta por el navegador en su peticion";
                        break;
                    case System.Net.HttpStatusCode.ProxyAuthenticationRequired:
                        mensaje = "el sever acepta la peticion pero se requiere la autenticacion del proxy";
                        break;
                    case System.Net.HttpStatusCode.RedirectKeepVerb:
                        mensaje = "la información de la solicitud se encuentra en el URI especificado en el encabezado";
                        break;
                    case System.Net.HttpStatusCode.RedirectMethod:
                        mensaje = "rediriguiendo automáticamente al cliente al URI especificado  ";
                        break;
                    case System.Net.HttpStatusCode.RequestedRangeNotSatisfiable:
                        mensaje = "la parte del archivo el server no la tiene ";
                        break;
                    case System.Net.HttpStatusCode.RequestEntityTooLarge:
                        mensaje = "la peticion del navegador es demasiado larga el server no lo procesa";
                        break;
                    case System.Net.HttpStatusCode.RequestTimeout:
                        mensaje = "fallo al continuar la peticion";
                        break;
                    case System.Net.HttpStatusCode.RequestUriTooLong:
                        mensaje = "el server no procesa la peticion por lo larga que esta";
                        break;
                    case System.Net.HttpStatusCode.ResetContent:
                        mensaje = "el  request se proceso correctamente pero no devuelve ningun contenido";
                        break;
                    case System.Net.HttpStatusCode.ServiceUnavailable:
                        mensaje = " el servidor no está disponible temporalmente";
                        break;
                    case System.Net.HttpStatusCode.SwitchingProtocols:
                        mensaje = "está cambiando la versión del protocolo o el protocolo.";
                        break;
                    case System.Net.HttpStatusCode.Unauthorized:
                        mensaje = "el recurso solicitado requiere autenticación. ";
                        break;
                    case System.Net.HttpStatusCode.UnsupportedMediaType:
                        mensaje = "indica que la solicitud es de un tipo no admitido.";
                        break;
                    case System.Net.HttpStatusCode.Unused:
                        mensaje = "no esta ulilizado";
                        break;
                    case System.Net.HttpStatusCode.UpgradeRequired:
                        mensaje = " el cliente debe cambiar a un protocolo diferente como TLS / 1.0";
                        break;
                    case System.Net.HttpStatusCode.UseProxy:
                        mensaje = "recurso solicitado solo a travez de proxy";
                        break;
                }
            }
            return mensaje;
        }
        #endregion
    }
}
