using lestoma.CommonUtils.Constants;
using lestoma.CommonUtils.Core;
using lestoma.CommonUtils.Interfaces;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace lestoma.Api.Helpers
{
    public class AuditoriaHelper : IAuditoriaHelper, IClaimsTransformation
    {
        private readonly ILoggerManager _logger;
        private readonly HttpContext _hcontext;
        protected readonly IDataProtector _protector;

        public AuditoriaHelper(IHttpContextAccessor hacess, ILoggerManager logger, IDataProtectionProvider protectorProvider)
        {
            _hcontext = hacess.HttpContext;
            _logger = logger;
            _protector = protectorProvider.CreateProtector(Constants.PROTECT_USER);
        }
        public string GetDesencrytedIp()
        {
            try
            {
                ClaimsIdentity claimsIdentity = new ClaimsIdentity();
                if (_hcontext != null)
                {
                    claimsIdentity = TransformAsync(_hcontext.User);

                }
                var claim = claimsIdentity?.FindFirst(x => x.Type == ClaimsConfig.IP);
                return claim == null ? "N/A" :

                    string.IsNullOrEmpty(claim.Value) ? "N/A" : _protector.Unprotect(claim.Value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return "N/A";
            }


        }

        public string GetTipoDeAplicacion()
        {
            try
            {
                ClaimsIdentity claimsIdentity = new ClaimsIdentity();
                if (_hcontext != null)
                {
                    claimsIdentity = TransformAsync(_hcontext.User);

                }
                var claim = claimsIdentity?.FindFirst(ClaimTypes.Authentication);
                return claim == null ? "Local" : string.IsNullOrEmpty(claim.Value) ? "Local" : claim.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return "Principal";
            }
        }

        public string GetSession()
        {
            try
            {
                ClaimsIdentity claimsIdentity = new ClaimsIdentity();
                if (_hcontext != null)
                {
                    claimsIdentity = TransformAsync(_hcontext.User);

                }
                var claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
                return claim == null ? "Anonimo" : string.IsNullOrEmpty(claim.Value) ? "Anonimo" : claim.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
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
