using lestoma.CommonUtils.Interfaces;
using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Claims;
using System.Threading;

namespace lestoma.CommonUtils.Helpers
{
    public class CamposAuditoriaHelper : ICamposAuditoriaHelper
    {
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
            ClaimsPrincipal principal = new ClaimsPrincipal();
            var identity = (ClaimsIdentity)principal.Identity;
            var claim = identity == null ? null : identity.FindFirst(ClaimTypes.Authentication);
            return claim == null ? "App Movil" : string.IsNullOrEmpty(claim.Value) ? "App Movil" : claim.Value; ;
        }

        public string ObtenerUsuarioActual()
        {
            ClaimsPrincipal principal = new ClaimsPrincipal();
            var identity = (ClaimsIdentity)principal.Identity;

            var claim = identity == null ? null : identity.FindFirst(ClaimTypes.Email);
            return claim == null ? "Anonimo" : string.IsNullOrEmpty(claim.Value) ? "Anonimo" : claim.Value;
        }
    }
}
