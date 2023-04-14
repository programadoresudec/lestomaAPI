using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using lestoma.CommonUtils.Constants;
using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.Interfaces;
using lestoma.CommonUtils.MyException;
using lestoma.CommonUtils.Requests;
using lestoma.Data.Repositories;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace lestoma.Logica.LogicaService
{
    public class UsuarioService : IUsuarioService
    {

        //ENVIAR CORREOS CON ARCHIVOS 
        //    await _mailHelper.SendMailWithOneArchive("diegop177@hotmail.com", "", "cedula.pdf", "activar cuenta",
        //MediaTypeNames.Application.Pdf, null, "cedula.pdf");
        private readonly ResponseDTO _respuesta = new();
        private readonly UsuarioRepository _usuarioRepository;
        private readonly AplicacionRepository _aplicacionRepository;
        private readonly UpaActividadRepository _upaActividadRepository;
        private readonly IMailHelper _mailHelper;
        private readonly IAmazonSimpleEmailService _amazonSimpleEmailService;
        public UsuarioService(UsuarioRepository usuarioRepository, IMailHelper mailHelper, IAmazonSimpleEmailService amazonSimpleEmailService,
            AplicacionRepository aplicacionRepository, UpaActividadRepository upaActividadRepository)
        {
            _usuarioRepository = usuarioRepository;
            _mailHelper = mailHelper;
            _aplicacionRepository = aplicacionRepository;
            _upaActividadRepository = upaActividadRepository;
            _amazonSimpleEmailService = amazonSimpleEmailService;

        }

        #region Get user
        public async Task<ResponseDTO> GetByIdAsync(int id)
        {
            var user = await _usuarioRepository.GetById(id);
            if (user == null) throw new HttpStatusCodeException(HttpStatusCode.NotFound, "usuario no encontrado");
            return Responses.SetOkResponse(user);
        }
        #endregion

        #region Iniciar sesión
        public async Task<ResponseDTO> Login(LoginRequest login)
        {
            var aplication = await _aplicacionRepository.AnyWithCondition(x => x.Id == login.TipoAplicacion);
            if (!aplication)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No se encuentra la aplicación registrada.");

            }
            var user = await _usuarioRepository.Logeo(login);
            if (user == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.Unauthorized, "correo y/o contraseña incorrectos.");
            }
            else
            {
                var upaId = await ValidateUser(user.Id, user.EstadoId, user.Rol.Id);
                if (HashHelper.CheckHash(login.Clave, user.Clave, user.Salt))
                {
                    _respuesta.MensajeHttp = "Ha iniciado satisfactoriamente.";
                    var refreshToken = generateRefreshToken(login.TipoAplicacion, user.Id, login.Ip);
                    user.RefreshToken = refreshToken.Token;
                    user.UpaId = upaId;
                    _respuesta.Data = user;
                    user.RefreshTokens.Add(refreshToken);
                    await _usuarioRepository.Update(user);
                    _respuesta.IsExito = true;
                    _respuesta.StatusCode = (int)HttpStatusCode.OK;
                }
                else
                {
                    throw new HttpStatusCodeException(HttpStatusCode.Unauthorized, "correo y/o contraseña incorrectos.");
                }
            }
            return _respuesta;
        }
        #endregion

        #region Validar usuario (tipo estado y rol)
        private async Task<Guid> ValidateUser(int userId, int estadoId, int rolId)
        {
            if (estadoId == (int)TipoEstadoUsuario.CheckCuenta)
            {
                throw new HttpStatusCodeException(HttpStatusCode.Unauthorized, "Su cuenta esta en proceso de activación.");
            }
            else if (estadoId == (int)TipoEstadoUsuario.Inactivo)
            {
                throw new HttpStatusCodeException(HttpStatusCode.Unauthorized, "Su cuenta esta Inactiva, debe comunicarse con el Super Administrador.");
            }
            else if (estadoId == (int)TipoEstadoUsuario.Bloqueado)
            {
                throw new HttpStatusCodeException(HttpStatusCode.Unauthorized, "Su cuenta esta bloqueada, debe comunicarse con el Super Administrador.");
            }
            var tieneUpa = await _upaActividadRepository.GetUpaByUserId(userId);
            if (tieneUpa == Guid.Empty && rolId != (int)TipoRol.SuperAdministrador)
            {
                throw new HttpStatusCodeException(HttpStatusCode.Unauthorized, "Su cuenta no cuenta con ninguna upa asociada comunicarse con el Super Administrador.");

            }
            return tieneUpa;
        }
        #endregion

        #region Generar el token para refrescar (en base 64 con parametros)
        private ETokensUsuarioByAplicacion generateRefreshToken(int tipoAplicacion, int id, string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new ETokensUsuarioByAplicacion
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow,
                    AplicacionId = tipoAplicacion,
                    UsuarioId = id,
                    CreatedByIp = ipAddress
                };
            }
        }
        #endregion

        #region Registrar un usuario dependiendo si lo hace el usuario o el super-admin por default se registra como rol auxiliar
        public async Task<ResponseDTO> RegisterUser(EUsuario usuario, bool ownRegister = true)
        {

            EUsuario existe = await _usuarioRepository.GetByEmail(usuario.Email);
            if (existe != null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.Conflict, "El correo ya está en uso.");
            }
            else
            {
                if (usuario.RolId == 0)
                {
                    usuario.RolId = (int)TipoRol.Auxiliar;
                }
                var hash = HashHelper.Hash(usuario.Clave);
                usuario.Apellido = usuario.Apellido.Trim();
                usuario.Nombre = usuario.Nombre.Trim();
                usuario.Clave = hash.Password;
                usuario.Salt = hash.Salt;
                if (usuario.EstadoId == 0)
                {
                    usuario.EstadoId = usuario.RolId == (int)TipoRol.Auxiliar ? (int)TipoEstadoUsuario.CheckCuenta : (int)TipoEstadoUsuario.Activado;
                }
                await _usuarioRepository.Create(usuario);
                _respuesta.IsExito = true;
                _respuesta.StatusCode = (int)HttpStatusCode.Created;
                _respuesta.MensajeHttp = ownRegister ? "Se ha registrado satisfactoriamente."
                    : "creado el usuario con exito.";
                if (usuario.RolId == (int)TipoRol.Auxiliar && ownRegister)
                {
                    //await _mailHelper.SendMail(usuario.Email, "Activación de Cuenta", String.Empty,
                    //     "Hola: ¡Su activación de la cuenta será pronto!",
                    //     "Su usuario se activará de acuerdo al super administrador.",
                    //     string.Empty, $"Enviamos este correo electrónico a {usuario.Email} porque te registraste en LESTOMA APP.", true);

                    await _mailHelper.SendMail(Constants.EMAIL_SUPER_ADMIN, $"Activación de cuenta: de {usuario.Email}", String.Empty,
                       "Hola: ¡Super Administrador!",
                       $"Debe activar la cuenta del auxiliar con correo: {usuario.Email} que se registro el dia: " +
                       $"{DateTime.Now.ToShortDateString()} a la hora: {DateTime.Now.ToShortTimeString()}",
                       string.Empty, $"LESTOMA APP", true);
                }
                else if (!ownRegister)
                {
                    string rol = usuario.RolId == (int)TipoRol.Auxiliar ? TipoRol.Auxiliar.ToString() : TipoRol.Administrador.ToString();

                    //await _mailHelper.SendMail(usuario.Email, "Se ha creado una cuenta en LESTOMA APP", String.Empty,
                    //   "Hola: ¡Logueate y conoce LESTOMA APP!",
                    //   $"Su usuario es: {usuario.Email} Contraseña: {clave}",
                    //   string.Empty, $"Enviamos este correo electrónico a {usuario.Email} porque te agregaron en LESTOMA APP.", true);

                    await _mailHelper.SendMail(Constants.EMAIL_SUPER_ADMIN, $"Registraste la cuenta: de {usuario.Email}", String.Empty,
                      "Hola: ¡Super Administrador!",
                      $"registraste el usuario con correo {usuario.Email} y rol {rol} el dia: " +
                      $"{DateTime.Now.ToShortDateString()} a la hora: {DateTime.Now.ToShortTimeString()}",
                      string.Empty, $"LESTOMA APP", true);
                }
            }
            return _respuesta;
        }
        #endregion

        #region Cambiar la contraseña
        public async Task<ResponseDTO> ChangePassword(ChangePasswordRequest cambiar)
        {
            var user = await _usuarioRepository.GetById(cambiar.IdUser);
            if (user == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "Te han eliminado.");
            }
            if (!HashHelper.CheckHash(cambiar.OldPassword, user.Clave, user.Salt))
            {
                throw new HttpStatusCodeException(HttpStatusCode.Conflict, "Verifique su contraseña actual.");
            }
            var hash = HashHelper.Hash(cambiar.NewPassword);
            user.Clave = hash.Password;
            user.Salt = hash.Salt;
            await _usuarioRepository.Update(user);
            _respuesta.MensajeHttp = "Se actualizo satisfactoriamente.";
            _respuesta.IsExito = true;
            _respuesta.StatusCode = (int)HttpStatusCode.OK;
            return _respuesta;
        }
        #endregion

        #region Generar correo por codigo de 6 digitos (se ha olvidado contraseña)
        public async Task<ResponseDTO> ForgotPassword(ForgotPasswordRequest email)
        {

            var user = await _usuarioRepository.GetByEmail(email.Email);
            if (user == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "El correo no esta registrado.");
            }
            else
            {
                bool validar;
                do
                {
                    user.CodigoRecuperacion = Reutilizables.GenerarCodigoVerificacion();
                    validar = await _usuarioRepository.ExisteCodigoVerificacion(user.CodigoRecuperacion);
                } while (validar != false);
                user.FechaVencimientoCodigo = DateTime.Now.AddHours(2);
                await _usuarioRepository.Update(user);
                _respuesta.Data = new ForgotPasswordDTO { Email = user.Email, CodigoVerificacion = user.CodigoRecuperacion };
                _respuesta.IsExito = true;
                _respuesta.MensajeHttp = "Revise su correo eléctronico.";
                await _mailHelper.SendMail(user.Email, $"{user.CodigoRecuperacion} es tu código de recuperación de contraseña", user.CodigoRecuperacion,
                    "Hola: ¡Cambia Tu Contraseña!",
                    "Verifica con el codigo tu cuenta para reestablecer la contraseña. el codigo tiene una duración de 2 horas.",
                    string.Empty, "Si no has intentado cambiar la contraseña con esta dirección de email recientemente," +
                    " puedes ignorar este mensaje.");
                _respuesta.StatusCode = (int)HttpStatusCode.OK;
            }
            return _respuesta;
        }

        #endregion

        #region Recuperar contraseña verifica el codigo que sea valido
        public async Task<ResponseDTO> RecoverPassword(RecoverPasswordRequest recover)
        {

            var user = await _usuarioRepository.VerifyCodeByRecover(recover.Codigo);
            if (user == null) throw new HttpStatusCodeException(HttpStatusCode.NotFound, "codigo no valido.");


            var hash = HashHelper.Hash(recover.Password);
            user.CodigoRecuperacion = null;
            user.Clave = hash.Password;
            user.Salt = hash.Salt;
            user.FechaVencimientoCodigo = null;
            await _usuarioRepository.Update(user);
            _respuesta.IsExito = true;
            _respuesta.MensajeHttp = "la contraseña ha sido restablecida.";
            _respuesta.StatusCode = (int)HttpStatusCode.OK;

            return _respuesta;
        }
        #endregion

        #region Revocar el token jwt
        public async Task<ResponseDTO> RevokeToken(string token, string ipAddress)
        {
            var user = _usuarioRepository.UsuarioByToken(token);

            // return false if no user found with token
            if (user == null) throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No se encuentra el usuario actual con token.");

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return false if token is not active
            if (!refreshToken.IsActive)
                throw new HttpStatusCodeException(HttpStatusCode.Conflict, "No se encuentra activo el token del usuario actual.");

            // revoke token and save
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            await _usuarioRepository.Update(user);
            return new ResponseDTO
            {
                MensajeHttp = "Token Revoked",
                IsExito = true,
                StatusCode = (int)HttpStatusCode.OK,
            };
        }

        #endregion

        #region Refrescar el token jwt con token base64
        public async Task<EUsuario> RefreshToken(string tokenRefresh, string ipAddress)
        {
            var user = _usuarioRepository.UsuarioByToken(tokenRefresh);

            // return null if no user found with token
            if (user == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "No se encuentra el usuario actual con token.");

            var refreshToken = user.RefreshTokens.Single(x => x.Token == tokenRefresh);

            // return null if token is no longer active
            if (!refreshToken.IsActive)
                throw new HttpStatusCodeException(HttpStatusCode.Conflict, "No se encuentra activo el token del usuario actual.");

            // replace old refresh token with a new one and save
            var newRefreshToken = generateRefreshToken(refreshToken.AplicacionId, refreshToken.UsuarioId, ipAddress);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            user.RefreshToken = newRefreshToken.Token;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            user.RefreshTokens.Add(newRefreshToken);
            await _usuarioRepository.Update(user);
            return user;
        }
        #endregion

        public async Task<IEnumerable<UserDTO>> GetUserswithoutUpa()
        {
            return await _usuarioRepository.GetUserswithoutUpa();
        }

        public short GetExpirationToken(int aplicacionId)
        {
            return _usuarioRepository.ExpiracionToken(aplicacionId);
        }

        public async Task<string> GetApplicationType(int tipoAplicacion)
        {
            return await _usuarioRepository.GetApplicationType(tipoAplicacion);
        }
        public async Task<IEnumerable<InfoUserDTO>> GetInfoUsers()
        {
            return await _usuarioRepository.GetInfoUsers();
        }

        public async Task<IEnumerable<EstadoDTO>> GetUserStatuses()
        {
            return await _usuarioRepository.GetUserStatuses();
        }

        public async Task<IEnumerable<RolDTO>> GetRoles()
        {
            return await _usuarioRepository.GetRoles();
        }
        public async Task<ResponseDTO> GetUpaUserId(int idUser)
        {
            var obj = await _upaActividadRepository.WhereWithCondition(x => x.UsuarioId == idUser).Include(y => y.Upa).FirstOrDefaultAsync();
            return obj == null
                ? throw new HttpStatusCodeException(HttpStatusCode.NotFound, "El usuario no cuenta con una upa asignada.")
                : Responses.SetOkResponse(new NameDTO { Id = obj.Upa.Id, Nombre = obj.Upa.Nombre });
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            return await _usuarioRepository.GetUsers();
        }
        public async Task<ResponseDTO> EditRol(RolRequest usuarioDTO)
        {
            var existe = await _usuarioRepository.GetById(usuarioDTO.IdUser);
            if (existe == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, "usuario no encontrado");
            }
            existe.RolId = usuarioDTO.RolUser;
            await _usuarioRepository.Update(existe);
            _respuesta.IsExito = true;
            _respuesta.MensajeHttp = "El rol ha sido editado.";
            _respuesta.StatusCode = (int)HttpStatusCode.OK;
            return _respuesta;

        }

        public async Task<ResponseDTO> UpdateUser(EUsuario usuario)
        {
            var respuesta = await GetByIdAsync(usuario.Id);
            EUsuario userActual = (EUsuario)respuesta.Data;
            userActual.Nombre = usuario.Nombre;
            userActual.RolId = usuario.RolId;
            userActual.Apellido = usuario.Apellido;
            userActual.EstadoId = usuario.EstadoId;
            await _usuarioRepository.Update(userActual);
            _respuesta.IsExito = true;
            _respuesta.MensajeHttp = "Usuario actualizado.";
            _respuesta.StatusCode = (int)HttpStatusCode.OK;
            return _respuesta;

        }

        #region Servicios AWS (tiene notificaciones activas, se pueden activar y desactivar notificaciones via email )
        public async Task<ResponseDTO> ActivateNotificationsMail(string email)
        {

            var response = await _amazonSimpleEmailService.VerifyEmailIdentityAsync(new VerifyEmailIdentityRequest { EmailAddress = email });
            return response.HttpStatusCode == HttpStatusCode.OK
                ? Responses.SetOkResponse(null, "Siga las instrucciones del correo enviado para activar las notificaciones via ¡Email!.")
                : Responses.SetOkResponse(null, "No es posible activar las notificaciones via ¡Email!.");
        }

        public async Task<ResponseDTO> UserIsActiveWithNotificationsMail(string email)
        {

            var response = await _amazonSimpleEmailService.GetIdentityVerificationAttributesAsync(new GetIdentityVerificationAttributesRequest { Identities = { email } });
            if (response.HttpStatusCode == HttpStatusCode.OK && response.VerificationAttributes.Count > 0)
            {
                IdentityVerificationAttributes identityVerificationAttributes;
                if (response.VerificationAttributes.TryGetValue(email, out identityVerificationAttributes))
                {
                    bool sucess = identityVerificationAttributes.VerificationStatus.Value == "Success" ? true : false;
                    return Responses.SetOkResponse(new HasNotificationsDTO
                    {
                        IsActive = sucess,
                        VerificationStatus = identityVerificationAttributes.VerificationStatus.Value
                    }, $"Está en estado {identityVerificationAttributes.VerificationStatus.Value} con las notificaciones via Email.");
                }
            }
            return Responses.SetOkResponse(new HasNotificationsDTO { IsActive = false, VerificationStatus = "NotExists" }, "No se encuentra activo en AWS.");
        }

        public async Task<ResponseDTO> DesactivateNotificationsMail(string email)
        {
            var response = await _amazonSimpleEmailService.DeleteIdentityAsync(new DeleteIdentityRequest { Identity = email });
            return response.HttpStatusCode == HttpStatusCode.OK
                ? Responses.SetOkResponse(null, "Se han desactivado las notificaciones via ¡Email!.")
                : throw new HttpStatusCodeException(HttpStatusCode.Conflict, "No se encuentra activado con las notificaciones Via ¡Email!.");
        }

        #endregion

    }
}
