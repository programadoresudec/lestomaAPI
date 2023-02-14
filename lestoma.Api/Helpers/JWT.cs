using AutoMapper;
using lestoma.CommonUtils.Constants;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.MyException;
using lestoma.CommonUtils.Requests.Filters;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
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
                UpaUserFilterRequest request = new UpaUserFilterRequest
                {
                    UpaId = user.UpaId,
                    UsuarioId = user.Id
                };
                var actividades = await _detalleUpaActividadService.GetActivitiesByUpaUserId(request);
                var claims = new List<Claim>
                {
                    new Claim(ClaimsConfig.ROL_ID, user.Rol.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Rol.NombreRol),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Sid, user.UserId),
                    new Claim(ClaimTypes.NameIdentifier, $"{user.Nombre} {user.Apellido}"),
                    new Claim(ClaimTypes.Authentication, user.TipoDeAplicacion),
                    new Claim(ClaimsConfig.APLICACION_ID, user.AplicacionId.ToString()),
                    new Claim(ClaimsConfig.UPA_ID, user.UpaId.ToString()),
                    new Claim(ClaimsConfig.IP, user.Ip.ToString())
                };

                // agregar actividades ID
                if (actividades.Any())
                {
                    foreach (var item in actividades)
                    {
                        claims.Add(new Claim(ClaimsConfig.PERMISOS_ACTIVIDADES_ID, item.Id.ToString()));
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
