﻿using AutoMapper;
using lestoma.Api.Helpers;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Interfaces;
using lestoma.CommonUtils.MyException;
using lestoma.CommonUtils.Requests;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
using lestoma.CommonUtils.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
            IMapper mapper, IJWT jwt, IDataProtectionProvider protectorProvider)
            : base(mapper, protectorProvider)
        {
            _usuarioService = usuarioService;
            _jwt = jwt;
        }
        #endregion

        #region refresh-token
        [AllowAnonymous]
        [HttpGet("IsSignIn")]
        public IActionResult IsSignIn()
        {
            var claims = ClaimsToken();
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Respuesta = new ResponseDTO
                {
                    IsExito = true,
                    MensajeHttp = "Esta Autenticado.",
                    StatusCode = (int)HttpStatusCode.OK
                };
                return Ok(Respuesta);
            }
            Respuesta = new ResponseDTO
            {
                IsExito = false,
                MensajeHttp = "No Esta Autenticado.",
                StatusCode = (int)HttpStatusCode.Unauthorized
            };
            return Unauthorized(Respuesta);
        }
        #endregion

        #region logeo
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Logeo(LoginRequest logeo)
        {
            Respuesta = await _usuarioService.Login(logeo, IpAddress());
            var data = (EUsuario)Respuesta.Data;
            data.AplicacionId = logeo.TipoAplicacion;
            data.TipoDeAplicacion = await _usuarioService.GetApplicationType(logeo.TipoAplicacion);
            data.Email = _protector.Protect(data.Email);
            data.UserId = _protector.Protect(data.Id.ToString());
            TokenDTO usuario = await _jwt.GenerateJwtToken(data);
            Respuesta.Data = usuario;
            SetTokenCookie(usuario.RefreshToken);
            SetAplicacionCookie(logeo.TipoAplicacion);

            //if (data.Rol.Id == (int)TipoRol.SuperAdministrador)
            //    await SignInUserHangfire($"{data.Nombre} {data.Apellido}");

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

            var response = await _usuarioService.RefreshToken(refreshToken, IpAddress());
            if (int.TryParse(idAplicacion, out int id))
            {
                sIdAplicacion = id;
            }
            response.TipoDeAplicacion = await _usuarioService.GetApplicationType(sIdAplicacion);
            TokenDTO usuario = await _jwt.GenerateJwtToken(response);
            SetTokenCookie(response.RefreshToken);
            SetAplicacionCookie(sIdAplicacion);
            Respuesta.Data = usuario;
            Respuesta.IsExito = true;
            Respuesta.StatusCode = (int)HttpStatusCode.OK;
            return Ok(Respuesta);
        }
        #endregion

        #region LogOut
        [HttpPost("LogOut")]
        [Authorize]
        public async Task<IActionResult> LogOut([FromBody] TokenRefreshDTO model)
        {
            // accept token from request body or cookie
            if (!model.Token.Equals(Request.Cookies["refreshToken"]))
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, $"El token de refresco no corresponde al actual.");
            }
            var response = await _usuarioService.RevokeToken(model.Token, IpAddress());
            return Ok(response);
        }
        #endregion

        #region Save cookies
        private void SetTokenCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }

        private void SetAplicacionCookie(int aplicacionId)
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
        [AllowAnonymous]
        public async Task<IActionResult> Registrarse(UsuarioRequest usuario)
        {
            var entidad = Mapear<UsuarioRequest, EUsuario>(usuario);
            Respuesta = await _usuarioService.RegisterUser(entidad);
            Respuesta.Data = usuario;
            return Created(string.Empty, Respuesta);
        }
        #endregion

        #region olvido su contraseña
        [HttpPut("forgotpassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest email)
        {
            Respuesta = await _usuarioService.ForgotPassword(email);
            return Ok(Respuesta);
        }
        #endregion

        #region restablecer la contraseña
        [HttpPut("recoverpassword")]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordRequest recover)
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
        [Authorize]
        public async Task<IActionResult> ChangeProfile(ChangeProfileRequest change)
        {
            Respuesta = await _usuarioService.ChangeProfile(change);
            return Ok(Respuesta);
        }
        #endregion

        private async Task SignInUserHangfire(string username)
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            identity.AddClaim(new Claim(ClaimTypes.Name, username ?? string.Empty));

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    AllowRefresh = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(1)
                });
        }
    }
}

