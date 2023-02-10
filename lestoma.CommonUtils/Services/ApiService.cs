using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.Interfaces;
using lestoma.CommonUtils.Requests;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace lestoma.CommonUtils.Services
{
    public class ApiService : IApiService
    {
        public HttpResponseMessage ResponseMessage { get; set; }
        private string _tokenNuevo;
        public ResponseDTO Respuesta { get; set; }

        #region Check conexion para consumo de servicios por internet

        public bool CheckConnection()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                return true;
            }

            return false;
        }

        #endregion

        #region Get httpclient

        private static HttpClient GetHttpClient(string urlBase)
        {
#if !DEBUG
                var httpClientHandler = new HttpClientHandler();

                httpClientHandler.ServerCertificateCustomValidationCallback =
                    (message, certificate, chain, sslPolicyErrors) => true;
                 
                return new HttpClient(httpClientHandler)
                {
                    Timeout = TimeSpan.FromSeconds(45),
                    BaseAddress = new Uri(urlBase),
                };
#endif

#if DEBUG

            return new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(45),
                BaseAddress = new Uri(urlBase),
            };
#endif
        }

        #endregion

        #region GetList Api service with token

        public async Task<ResponseDTO> GetListAsyncWithToken<T>(string urlBase, string nameService, string token)
        {
            try
            {
                HttpClient client = GetHttpClient(urlBase);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                ResponseMessage = await client.GetAsync(nameService);
                string jsonString = await ResponseMessage.Content.ReadAsStringAsync();
                if (!ResponseMessage.IsSuccessStatusCode)
                {
                    Respuesta = JsonConvert.DeserializeObject<ResponseDTO>(jsonString);
                    return new ResponseDTO
                    {
                        IsExito = false,
                        MensajeHttp =
                            mostrarMensajePersonalizadoStatus(ResponseMessage.StatusCode, Respuesta.MensajeHttp)
                    };
                }
                else if (ResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await RefreshToken(urlBase);
                    await GetListAsyncWithToken<T>(urlBase, nameService, _tokenNuevo);
                }

                T item = JsonConvert.DeserializeObject<T>(jsonString);
                if (item == null)
                {
                    return new ResponseDTO
                    {
                        IsExito = true,
                        MensajeHttp = "No hay contenido."
                    };
                }

                return new ResponseDTO
                {
                    IsExito = true,
                    Data = item
                };
            }
            catch (Exception ex)
            {
                var jsonError = JsonConvert.SerializeObject(new ResponseDTO
                {
                    IsExito = false,
                    StatusCode = ResponseMessage != null
                        ? (int)ResponseMessage.StatusCode
                        : (int)HttpStatusCode.InternalServerError,
                    MensajeHttp = ResponseMessage != null
                        ? mostrarMensajePersonalizadoStatus(ResponseMessage.StatusCode, string.Empty)
                        : ex.Message
                });
                throw new Exception(jsonError);
            }
        }

        #endregion

        #region Get By Id Api service with token

        public async Task<ResponseDTO> GetByIdAsyncWithToken(string urlBase, string nameService, string token)
        {
            try
            {
                HttpClient client = GetHttpClient(urlBase);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                ResponseMessage = await client.GetAsync(nameService);
                string jsonString = await ResponseMessage.Content.ReadAsStringAsync();
                if (!ResponseMessage.IsSuccessStatusCode)
                {
                    Respuesta = JsonConvert.DeserializeObject<ResponseDTO>(jsonString);
                    return new ResponseDTO
                    {
                        IsExito = false,
                        MensajeHttp =
                          mostrarMensajePersonalizadoStatus(ResponseMessage.StatusCode, Respuesta.MensajeHttp)
                    };
                }
                else if (ResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await RefreshToken(urlBase);
                    await GetByIdAsyncWithToken(urlBase, nameService, _tokenNuevo);
                }

                ResponseDTO item = JsonConvert.DeserializeObject<ResponseDTO>(jsonString);
                return item;
            }
            catch (Exception ex)
            {
                var jsonError = JsonConvert.SerializeObject(new ResponseDTO
                {
                    IsExito = false,
                    StatusCode = ResponseMessage != null
                        ? (int)ResponseMessage.StatusCode
                        : (int)HttpStatusCode.InternalServerError,
                    MensajeHttp = ResponseMessage != null
                        ? mostrarMensajePersonalizadoStatus(ResponseMessage.StatusCode, string.Empty)
                        : ex.Message
                });
                throw new Exception(jsonError);
            }
        }

        #endregion

        #region Get paginado Api service with token

        public async Task<ResponseDTO> GetPaginadoAsyncWithToken<T>(string urlBase, string nameService, string token)
        {
            try
            {
                HttpClient client = GetHttpClient(urlBase);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                ResponseMessage = await client.GetAsync(nameService);
                string jsonString = await ResponseMessage.Content.ReadAsStringAsync();
                if (!ResponseMessage.IsSuccessStatusCode)
                {
                    Respuesta = JsonConvert.DeserializeObject<ResponseDTO>(jsonString);
                    return new ResponseDTO
                    {
                        IsExito = false,
                        MensajeHttp =
                            mostrarMensajePersonalizadoStatus(ResponseMessage.StatusCode, Respuesta.MensajeHttp)
                    };
                }
                else if (ResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await RefreshToken(urlBase);
                    await GetListAsyncWithToken<T>(urlBase, nameService, _tokenNuevo);
                }

                Paginador<T> item = JsonConvert.DeserializeObject<Paginador<T>>(jsonString);
                if (item == null)
                {
                    return new ResponseDTO
                    {
                        IsExito = true,
                        MensajeHttp = "No hay contenido."
                    };
                }

                return new ResponseDTO
                {
                    IsExito = true,
                    Data = item
                };
            }
            catch (Exception ex)
            {
                var jsonError = JsonConvert.SerializeObject(new ResponseDTO
                {
                    IsExito = false,
                    StatusCode = ResponseMessage != null
                        ? (int)ResponseMessage.StatusCode
                        : (int)HttpStatusCode.InternalServerError,
                    MensajeHttp = ResponseMessage != null
                        ? mostrarMensajePersonalizadoStatus(ResponseMessage.StatusCode, string.Empty)
                        : ex.Message
                });
                throw new Exception(jsonError);
            }
        }

        #endregion

        #region Post Api service

        public async Task<ResponseDTO> PostAsync<T>(string urlBase, string nameService, T model)
        {
            try
            {
                HttpClient client = GetHttpClient(urlBase);
                string json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                ResponseMessage = await client.PostAsync(nameService, content);
                string jsonString = await ResponseMessage.Content.ReadAsStringAsync();
                Respuesta = JsonConvert.DeserializeObject<ResponseDTO>(jsonString);
                if (!ResponseMessage.IsSuccessStatusCode)
                {
                    return new ResponseDTO
                    {
                        IsExito = false,
                        StatusCode = (int)ResponseMessage.StatusCode,
                        MensajeHttp =
                            mostrarMensajePersonalizadoStatus(ResponseMessage.StatusCode, Respuesta.MensajeHttp)
                    };
                }

                return Respuesta;
            }
            catch (Exception ex)
            {
                var jsonError = JsonConvert.SerializeObject(new ResponseDTO
                {
                    IsExito = false,
                    StatusCode = ResponseMessage != null
                        ? (int)ResponseMessage.StatusCode
                        : (int)HttpStatusCode.InternalServerError,
                    MensajeHttp = ResponseMessage != null
                        ? mostrarMensajePersonalizadoStatus(ResponseMessage.StatusCode, string.Empty)
                        : ex.Message
                });
                throw new Exception(jsonError);
            }
        }

        #endregion

        #region Post Api service with token
        public async Task<ResponseDTO> PostAsyncWithToken<T>(string urlBase, string nameService, T model, string token)
        {
            try
            {
                HttpClient client = GetHttpClient(urlBase);
                string json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                ResponseMessage = await client.PostAsync(nameService, content);
                string jsonString = await ResponseMessage.Content.ReadAsStringAsync();
                Respuesta = JsonConvert.DeserializeObject<ResponseDTO>(jsonString);
                if (!ResponseMessage.IsSuccessStatusCode)
                {
                    return new ResponseDTO
                    {
                        IsExito = false,
                        MensajeHttp =
                            mostrarMensajePersonalizadoStatus(ResponseMessage.StatusCode, Respuesta.MensajeHttp)
                    };
                }
                else if (ResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await RefreshToken(urlBase);
                    await PostAsyncWithToken(urlBase, nameService, model, _tokenNuevo);
                }

                return Respuesta;
            }
            catch (Exception ex)
            {
                var jsonError = JsonConvert.SerializeObject(new ResponseDTO
                {
                    IsExito = false,
                    StatusCode = ResponseMessage != null
                        ? (int)ResponseMessage.StatusCode
                        : (int)HttpStatusCode.InternalServerError,
                    MensajeHttp = ResponseMessage != null
                        ? mostrarMensajePersonalizadoStatus(ResponseMessage.StatusCode, string.Empty)
                        : ex.Message
                });
                throw new Exception(jsonError);
            }
        }

        #endregion

        #region Post Api without body service with token
        public async Task<ResponseDTO> PostWithoutBodyAsyncWithToken(string urlBase, string nameService, string token)
        {
            try
            {
                HttpClient client = GetHttpClient(urlBase);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                ResponseMessage = await client.PostAsync(nameService, null);
                string jsonString = await ResponseMessage.Content.ReadAsStringAsync();
                Respuesta = JsonConvert.DeserializeObject<ResponseDTO>(jsonString);
                if (!ResponseMessage.IsSuccessStatusCode)
                {
                    return new ResponseDTO
                    {
                        IsExito = false,
                        MensajeHttp =
                            mostrarMensajePersonalizadoStatus(ResponseMessage.StatusCode, Respuesta.MensajeHttp)
                    };
                }
                else if (ResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await RefreshToken(urlBase);
                    await PostWithoutBodyAsyncWithToken(urlBase, nameService, _tokenNuevo);
                }

                return Respuesta;
            }
            catch (Exception ex)
            {
                var jsonError = JsonConvert.SerializeObject(new ResponseDTO
                {
                    IsExito = false,
                    StatusCode = ResponseMessage != null
                        ? (int)ResponseMessage.StatusCode
                        : (int)HttpStatusCode.InternalServerError,
                    MensajeHttp = ResponseMessage != null
                        ? mostrarMensajePersonalizadoStatus(ResponseMessage.StatusCode, string.Empty)
                        : ex.Message
                });
                throw new Exception(jsonError);
            }
        }

        #endregion

        #region Put Api service

        public async Task<ResponseDTO> PutAsync<T>(string urlBase, string nameService, T model)
        {
            try
            {
                HttpClient client = GetHttpClient(urlBase);
                string json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                ResponseMessage = await client.PutAsync(nameService, content);
                string jsonString = await ResponseMessage.Content.ReadAsStringAsync();
                Respuesta = JsonConvert.DeserializeObject<ResponseDTO>(jsonString);
                if (!ResponseMessage.IsSuccessStatusCode)
                {
                    return new ResponseDTO
                    {
                        IsExito = false,
                        StatusCode = (int)ResponseMessage.StatusCode,
                        MensajeHttp =
                            mostrarMensajePersonalizadoStatus(ResponseMessage.StatusCode, Respuesta.MensajeHttp)
                    };
                }

                return Respuesta;
            }
            catch (Exception ex)
            {
                var jsonError = JsonConvert.SerializeObject(new ResponseDTO
                {
                    IsExito = false,
                    StatusCode = ResponseMessage != null
                        ? (int)ResponseMessage.StatusCode
                        : (int)HttpStatusCode.InternalServerError,
                    MensajeHttp = ResponseMessage != null
                        ? mostrarMensajePersonalizadoStatus(ResponseMessage.StatusCode, string.Empty)
                        : ex.Message
                });
                throw new Exception(jsonError);
            }
        }

        #endregion

        #region Put Api service with token

        public async Task<ResponseDTO> PutAsyncWithToken<T>(string urlBase, string nameService, T model, string token)
        {
            try
            {
                HttpClient client = GetHttpClient(urlBase);
                string json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                ResponseMessage = await client.PutAsync(nameService, content);
                string jsonString = await ResponseMessage.Content.ReadAsStringAsync();
                Respuesta = JsonConvert.DeserializeObject<ResponseDTO>(jsonString);
                if (!ResponseMessage.IsSuccessStatusCode)
                {
                    return new ResponseDTO
                    {
                        IsExito = false,
                        MensajeHttp =
                            mostrarMensajePersonalizadoStatus(ResponseMessage.StatusCode, Respuesta.MensajeHttp)
                    };
                }

                return Respuesta;
            }
            catch (Exception ex)
            {
                var jsonError = JsonConvert.SerializeObject(new ResponseDTO
                {
                    IsExito = false,
                    StatusCode = ResponseMessage != null
                        ? (int)ResponseMessage.StatusCode
                        : (int)HttpStatusCode.InternalServerError,
                    MensajeHttp = ResponseMessage != null
                        ? mostrarMensajePersonalizadoStatus(ResponseMessage.StatusCode, string.Empty)
                        : ex.Message
                });
                throw new Exception(jsonError);
            }
        }

        #endregion

        #region Delete Api service with token

        public async Task<ResponseDTO> DeleteAsyncWithToken(string urlBase, string nameService, object id, string token)
        {
            try
            {
                HttpClient client = GetHttpClient(urlBase);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                ResponseMessage = await client.DeleteAsync($"{nameService}/{id}");
                string jsonString = await ResponseMessage.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(jsonString))
                {
                    Respuesta = JsonConvert.DeserializeObject<ResponseDTO>(jsonString);
                }

                if (!ResponseMessage.IsSuccessStatusCode)
                {
                    return new ResponseDTO
                    {
                        IsExito = false,
                        StatusCode = (int)ResponseMessage.StatusCode,
                        MensajeHttp =
                            mostrarMensajePersonalizadoStatus(ResponseMessage.StatusCode, Respuesta.MensajeHttp)
                    };
                }
                else if (ResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await RefreshToken(urlBase);
                    await DeleteAsyncWithToken(urlBase, nameService, id, _tokenNuevo);
                }

                return new ResponseDTO
                {
                    IsExito = true,
                    MensajeHttp = "se ha eliminado correctamente."
                };
            }
            catch (Exception ex)
            {
                var jsonError = JsonConvert.SerializeObject(new ResponseDTO
                {
                    IsExito = false,
                    StatusCode = ResponseMessage != null
                        ? (int)ResponseMessage.StatusCode
                        : (int)HttpStatusCode.InternalServerError,
                    MensajeHttp = ResponseMessage != null
                        ? mostrarMensajePersonalizadoStatus(ResponseMessage.StatusCode, string.Empty)
                        : ex.Message
                });
                throw new Exception(jsonError);
            }
        }

        #endregion

        #region RefreshToken

        private async Task RefreshToken(string urlBase)
        {
            try
            {
                HttpClient client = GetHttpClient(urlBase);
                TipoAplicacionRequest tipoAplicacionRequest = new TipoAplicacionRequest
                {
                    TipoAplicacion = (int)TipoAplicacion.AppMovil
                };
                var json = JsonConvert.SerializeObject(tipoAplicacionRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                ResponseMessage = await client.PostAsync("Account/refresh-token", content);
                ResponseMessage.EnsureSuccessStatusCode();
                string jsonString = await ResponseMessage.Content.ReadAsStringAsync();
                Respuesta = JsonConvert.DeserializeObject<ResponseDTO>(jsonString);
                TokenDTO tokenNuevo = (TokenDTO)Respuesta.Data;
                MovilSettings.Token = JsonConvert.SerializeObject(tokenNuevo);
                _tokenNuevo = tokenNuevo.Token;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
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
                        mensaje =
                            "el server no puede cumplir con alguna condicion impuesta por el navegador en su peticion";
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