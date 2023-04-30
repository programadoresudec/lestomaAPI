using Hangfire.Dashboard;
using lestoma.CommonUtils.Constants;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.MyException;
using Microsoft.AspNetCore.Http;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;

namespace lestoma.Api.Core
{
    public class HangfireDashboardJwtAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private static void SetCookie(string jwtToken, DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            httpContext.Response.Cookies.Append("_hangfireCookie",
                    jwtToken,
                    new CookieOptions()
                    {
                        Secure = true,
                        Expires = DateTime.Now.AddHours(1)
                    });
        }

        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            var jwtToken = String.Empty;

            if (httpContext.Request.Query.ContainsKey("jwt_token"))
            {
                jwtToken = httpContext.Request.Query["jwt_token"].FirstOrDefault();
                SetCookie(jwtToken, context);
            }
            else
            {
                jwtToken = httpContext.Request.Cookies["_hangfireCookie"];
            }

            if (String.IsNullOrEmpty(jwtToken))
            {
                return false;
            }

            JwtSecurityTokenHandler handler = new();
            JwtSecurityToken jwtSecurityToken = handler.ReadJwtToken(jwtToken);
            try
            {
                if (jwtSecurityToken.ValidTo < DateTime.UtcNow)
                {
                    return false;
                }
                int IdRol = 0;
                var rolId = jwtSecurityToken.Claims.Where(x => x.Type == ClaimsConfig.ROL_ID)
                    .Select(x => x.Value).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(rolId))
                    return false;
                if (int.TryParse(rolId, out int RolId))
                {
                    IdRol = RolId;  
                }

                return IdRol == (int)TipoRol.SuperAdministrador;
            }
            catch (Exception exception)
            {
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, exception);
            }
        }
    }
}
