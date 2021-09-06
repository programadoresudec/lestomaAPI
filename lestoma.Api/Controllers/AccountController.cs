using AutoMapper;
using lestoma.Api.Helpers;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.Requests;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace lestoma.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        #region attributes
        private readonly AppSettings _appSettings;
        private readonly IUsuarioService _usuarioService;
        private readonly IMailHelper _mailHelper;
        #endregion

        #region Constructor
        public AccountController(IUsuarioService usuarioService,
            IOptions<AppSettings> appSettings, IMailHelper mailHelper, IMapper mapper)
            : base(mapper)
        {
            _mailHelper = mailHelper;
            _usuarioService = usuarioService;
            _appSettings = appSettings.Value;
        }
        #endregion

        [Authorize(Roles = "Administrador")]
        [HttpGet("Usuarios")]
        public async Task<IActionResult> Lista()
        {
            Respuesta = await _usuarioService.lista();
            return Ok(Respuesta);
        }
        #region logeo
        [HttpPost("login")]
        public async Task<IActionResult> Logeo(LoginRequest logeo)
        {
            Respuesta = await _usuarioService.Login(logeo, ipAddress());
            if (Respuesta.Data == null)
            {
                return Unauthorized(Respuesta);
            }
            var data = (EUsuario)Respuesta.Data;
            data.AplicacionId = logeo.TipoAplicacion;
            TokenDTO usuario = GetToken(data);
            Respuesta.Data = usuario;
            setTokenCookie(usuario.RefreshToken);
            return Ok(Respuesta);
        }
        #endregion

        #region refresh-token
        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _usuarioService.RefreshToken(refreshToken, ipAddress());

            if (response == null)
                return Unauthorized(new { message = "Invalid token" });

            TokenDTO usuario = GetToken(response);
            setTokenCookie(response.RefreshToken);
            Respuesta = new Response();
            Respuesta.Data = usuario;
            Respuesta.IsExito = true;
            return Ok(Respuesta);
        }
        #endregion

        #region revoke-token

        [HttpPost("revoke-token")]
        public IActionResult RevokeToken([FromBody] RevokeTokenRequest model)
        {
            // accept token from request body or cookie
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            var response = _usuarioService.RevokeToken(token, ipAddress());

            if (!response)
                return NotFound(new { message = "Token not found" });

            return Ok(new { message = "Token revoked" });
        }
        private void setTokenCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
        #endregion

        #region registrarse
        [HttpPost("registro")]
        public async Task<IActionResult> Registrarse(UsuarioRequest usuario)
        {
            var entidad = Mapear<UsuarioRequest, EUsuario>(usuario);
            Respuesta = await _usuarioService.Register(entidad);
            if (!Respuesta.IsExito)
            {
                return Conflict(Respuesta);
            }
            Respuesta.Data = usuario;
            return Created(string.Empty, Respuesta);
        }
        #endregion

        #region olvido su contraseña
        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest email)
        {
            Respuesta = await _usuarioService.ForgotPassword(email);
            if (!Respuesta.IsExito && Respuesta.Data == null)
            {
                return Conflict(Respuesta);
            }
            var from = ((EUsuario)Respuesta.Data).Email;
            var codigo = ((EUsuario)Respuesta.Data).CodigoRecuperacion;

            await _mailHelper.SendCorreo(from, codigo, "Recuperación contraseña");
            return Created(string.Empty, Respuesta);
        }
        #endregion

        #region restablecer la contraseña
        [HttpPost("recoverpassword")]
        public async Task<IActionResult> ForgotPassword(RecoverPasswordRequest recover)
        {
            Respuesta = await _usuarioService.RecoverPassword(recover);
            if (!Respuesta.IsExito)
            {
                return Conflict(Respuesta);
            }
            return Created(string.Empty, Respuesta);
        }
        #endregion

        #region cambiar la contraseña
        [Authorize]
        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest change)
        {
            Respuesta = await _usuarioService.ChangePassword(change);
            if (!Respuesta.IsExito)
            {
                return Conflict(Respuesta);
            }
            return Created(string.Empty, Respuesta);
        }
        #endregion

        #region cambiar el perfil
        [HttpPost("changeprofile")]
        public async Task<IActionResult> ChangeProfile(ChangeProfileRequest change)
        {
            Respuesta = await _usuarioService.ChangeProfile(change);
            if (!Respuesta.IsExito)
            {
                return Conflict(Respuesta);
            }
            return Created(string.Empty, Respuesta);
        }
        #endregion

        #region Generar token JWT
        private TokenDTO GetToken(EUsuario user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var llave = Encoding.ASCII.GetBytes(_appSettings.Secreto);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity
                (
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Role, user.Rol.NombreRol),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Authentication, user.TipoDeAplicacion)
                    }
                ),
                Audience = _appSettings.Audience,
                Issuer = _appSettings.Issuer,
                Expires = user.AplicacionId == (int)TipoAplicacion.AppMovil ?
                DateTime.UtcNow.AddDays(_usuarioService.GetExpiracionToken(user.AplicacionId)) : user.AplicacionId == (int)TipoAplicacion.Web ?
                 DateTime.UtcNow.AddMinutes(_usuarioService.GetExpiracionToken(user.AplicacionId)) : DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            TokenDTO userWithToken = new()
            {
                Token = tokenHandler.WriteToken(token),
                Expiration = token.ValidTo,
                RefreshToken = user.RefreshToken,
                User = Mapear<EUsuario, UserDTO>(user)
            };
            return userWithToken;
        }
        #endregion

        #region Ip
        private string ipAddress()
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
        #endregion
    }
}

