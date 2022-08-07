using AutoMapper;
using lestoma.CommonUtils.Constants;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.MyException;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace lestoma.Api.Helpers
{
    public interface IJWT
    {
        Task<TokenDTO> GenerateJwtToken(EUsuario user);
    }
    public class JWT : IJWT
    {
        public readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IUsuarioService _usuarioService;
        private readonly IDetalleUpaActividadService _detalleUpaActividadService;
        public JWT(IUsuarioService usuarioService,
            IOptions<AppSettings> appSettings, IMapper mapper, IDetalleUpaActividadService detalleUpaActividadService)
        {
            _appSettings = appSettings.Value;
            _usuarioService = usuarioService;
            _mapper = mapper;
            _detalleUpaActividadService = detalleUpaActividadService;
        }

        #region Generar token JWT
        public async Task<TokenDTO> GenerateJwtToken(EUsuario user)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var llave = Encoding.ASCII.GetBytes(_appSettings.Secreto);
                var actividades = await _detalleUpaActividadService.GetActivities(user.Id, user.UpaId);
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimsConfig.ID_ROL, user.Rol.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Role, user.Rol.NombreRol));
                claims.Add(new Claim(ClaimTypes.Email, user.Email));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, $"{user.Nombre} {user.Apellido}"));
                claims.Add(new Claim(ClaimTypes.Authentication, user.TipoDeAplicacion));
                claims.Add(new Claim(ClaimsConfig.ID_APLICACION, user.AplicacionId.ToString()));
                claims.Add(new Claim(ClaimsConfig.ID_UPA, user.UpaId.ToString()));
                if (actividades.Count() > 0)
                {
                    foreach (var item in actividades)
                    {
                        claims.Add(new Claim(ClaimsConfig.ROLES_ACTIVIDADES, item));
                    }
                }
                var tokenDescriptor = new SecurityTokenDescriptor
                {

                    Subject = new ClaimsIdentity
                    (
                      claims
                    ),
                    Audience = _appSettings.Audience,
                    Issuer = _appSettings.Issuer,
                    Expires = user.AplicacionId == (int)TipoAplicacion.AppMovil ?
                    DateTime.UtcNow.AddDays(_usuarioService.GetExpirationToken(user.AplicacionId)) : user.AplicacionId == (int)TipoAplicacion.Web ?
                     DateTime.UtcNow.AddMinutes(_usuarioService.GetExpirationToken(user.AplicacionId)) : DateTime.UtcNow.AddMinutes(15),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                TokenDTO userWithToken = new()
                {
                    Token = tokenHandler.WriteToken(token),
                    Expiration = token.ValidTo,
                    RefreshToken = user.RefreshToken,
                    User = _mapper.Map<UserDTO>(user)
                };
                return userWithToken;
            }
            catch (Exception ex)
            {
                throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, $"no se ha podido crear el token. {ex.Message}");
            }
        }
        #endregion
    }
}
