using Hangfire.Dashboard;
using lestoma.CommonUtils.Core;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.MyException;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;

namespace lestoma.Api.Core
{
    public class HangfireDashboardJwtAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private void SetCookie(string jwtToken, DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            httpContext.Response.Cookies.Append("_hangfireCookie",
                    jwtToken,
                    new CookieOptions()
                    {
                        Expires = DateTime.Now.AddMinutes(30)
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

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(jwtToken);

            try
            {
                // Only authenticated users who have the required claim (AzureAD group in this demo) can access the dashboard.
                return jwtSecurityToken.Claims.Any(t => t.Type == ClaimTypes.Role && t.Value == EnumConfig.GetDescription(TipoRol.SuperAdministrador));
            }
            catch (Exception exception)
            {
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, exception);
            }
        }
    }
}
