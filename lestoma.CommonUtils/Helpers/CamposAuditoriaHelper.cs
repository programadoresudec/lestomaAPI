using lestoma.Api.Helpers;
using lestoma.CommonUtils.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Security.Claims;

namespace lestoma.CommonUtils.Helpers
{
    public class CamposAuditoriaHelper : ICamposAuditoriaHelper, IClaimsTransformation
    {
        private HttpContext hcontext;

        public CamposAuditoriaHelper(IHttpContextAccessor hacess)
        {
            hcontext = hacess.HttpContext;
        }
        public string ObtenerIp()
        {
            string IP4Address = String.Empty;
            foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (IPA.AddressFamily == AddressFamily.InterNetwork)
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }
            return IP4Address;

        }

        public string ObtenerTipoDeAplicacion()
        {
            try
            {
                ClaimsIdentity claimsIdentity = new ClaimsIdentity();
                if (hcontext != null)
                {
                    claimsIdentity = TransformAsync(hcontext.User);

                }
                var claim = claimsIdentity == null ? null : claimsIdentity.FindFirst(ClaimTypes.Authentication);
                return claim == null ? "Local" : string.IsNullOrEmpty(claim.Value) ? "Local" : claim.Value; ;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return "Principal";
            }

        }

        public string ObtenerUsuarioActual()
        {
            try
            {
                ClaimsIdentity claimsIdentity = new ClaimsIdentity();
                if (hcontext != null)
                {
                    claimsIdentity = TransformAsync(hcontext.User);

                }
                var claim = claimsIdentity == null ? null : claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                return claim == null ? "Anonimo" : string.IsNullOrEmpty(claim.Value) ? "Anonimo" : claim.Value;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return "anonimo";
            }
        }

        public ClaimsIdentity TransformAsync(ClaimsPrincipal principal)
        {
            var identity = (ClaimsIdentity)principal.Identity;
            return identity;
        }
    }
}
