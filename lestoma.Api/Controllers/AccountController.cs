using AutoMapper;
using lestoma.Api.Helpers;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Interfaces;
using lestoma.CommonUtils.MyException;
using lestoma.CommonUtils.Requests;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace lestoma.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        #region attributes
        private readonly IUsuarioService _usuarioService;
        private readonly IJWT _jwt;
        #endregion

        #region Constructor
        public AccountController(IUsuarioService usuarioService,
            IMapper mapper, IJWT jwt)
            : base(mapper)
        {
            _usuarioService = usuarioService;
            _jwt = jwt;
        }
        #endregion

        #region logeo
        [HttpPost("login")]
        public async Task<IActionResult> Logeo(LoginRequest logeo)
        {
            Respuesta = await _usuarioService.Login(logeo, ipAddress());
            var data = (EUsuario)Respuesta.Data;
            data.AplicacionId = logeo.TipoAplicacion;
            data.TipoDeAplicacion = await _usuarioService.GetApplicationType(logeo.TipoAplicacion);
            TokenDTO usuario = await _jwt.GenerateJwtToken(data);
            Respuesta.Data = usuario;
            setTokenCookie(usuario.RefreshToken);
            setAplicacionCookie(logeo.TipoAplicacion);
            return Created(string.Empty, Respuesta);
        }
        #endregion

        #region refresh-token
        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            int sIdAplicacion = 0;
            var refreshToken = Request.Cookies["refreshToken"];
            var idAplicacion = Request.Cookies["aplicacionId"];

            var response = await _usuarioService.RefreshToken(refreshToken, ipAddress());
            if (int.TryParse(idAplicacion, out int id))
            {
                sIdAplicacion = id;
            }
            response.TipoDeAplicacion = await _usuarioService.GetApplicationType(sIdAplicacion);
            TokenDTO usuario = await _jwt.GenerateJwtToken(response);
            setTokenCookie(response.RefreshToken);
            setAplicacionCookie(sIdAplicacion);
            Respuesta.Data = usuario;
            Respuesta.IsExito = true;
            Respuesta.StatusCode = (int)HttpStatusCode.OK;
            return Ok(Respuesta);
        }
        #endregion

        #region LogOut

        [HttpPost("LogOut")]
        public async Task<IActionResult> LogOut([FromBody] TokenRefreshDTO model)
        {
            // accept token from request body or cookie
            if (!model.Token.Equals(Request.Cookies["refreshToken"]))
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, $"El token de refresco no corresponde al actual.");
            }
            var response = await _usuarioService.RevokeToken(model.Token, ipAddress());
            return Ok(response);
        }
        #endregion

        #region Save cookies
        private void setTokenCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        private void setAplicacionCookie(int aplicacionId)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("aplicacionId", aplicacionId.ToString(), cookieOptions);
        }

        #endregion

        #region registrarse
        [HttpPost("registro")]
        public async Task<IActionResult> Registrarse(UsuarioRequest usuario)
        {
            var entidad = Mapear<UsuarioRequest, EUsuario>(usuario);
            Respuesta = await _usuarioService.Register(entidad);
            Respuesta.Data = usuario;
            return Created(string.Empty, Respuesta);
        }
        #endregion

        #region olvido su contraseña
        [HttpPut("forgotpassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest email)
        {
            Respuesta = await _usuarioService.ForgotPassword(email);
            return Ok(Respuesta);
        }
        #endregion

        #region restablecer la contraseña
        [HttpPut("recoverpassword")]
        public async Task<IActionResult> ForgotPassword(RecoverPasswordRequest recover)
        {
            Respuesta = await _usuarioService.RecoverPassword(recover);
            Respuesta.StatusCode = (int)HttpStatusCode.OK;
            return Ok(Respuesta);
        }
        #endregion

        #region cambiar la contraseña
        [Authorize]
        [HttpPut("changepassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest change)
        {
            Respuesta = await _usuarioService.ChangePassword(change);
            Respuesta.StatusCode = (int)HttpStatusCode.OK;
            return Ok(Respuesta);
        }
        #endregion

        #region cambiar el perfil
        [HttpPut("changeprofile")]
        public async Task<IActionResult> ChangeProfile(ChangeProfileRequest change)
        {
            Respuesta = await _usuarioService.ChangeProfile(change);
            return Ok(Respuesta);
        }
        #endregion
    }
}

