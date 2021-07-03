using AutoMapper;
using lestoma.Api.Helpers;
using lestoma.CommonUtils.Entities;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.Requests;
using lestoma.CommonUtils.Responses;
using lestoma.Logica.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
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
            Respuesta = await _usuarioService.Login(logeo);
            if (Respuesta.Data == null)
            {
                return Unauthorized(Respuesta);
            }
            TokenResponse usuario = GetToken((EUsuario)Respuesta.Data);
            Respuesta.Data = usuario;
            return Ok(Respuesta);
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
        private TokenResponse GetToken(EUsuario user)
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
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email)
                    }
                ),
                Audience = _appSettings.Audience,
                Issuer = _appSettings.Issuer,
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            TokenResponse userWithToken = new()
            {
                Token = tokenHandler.WriteToken(token),
                Expiration = token.ValidTo,
                User = Mapear<EUsuario, UserResponse>(user)
            };
            return userWithToken;
        }
        #endregion
    }
}

