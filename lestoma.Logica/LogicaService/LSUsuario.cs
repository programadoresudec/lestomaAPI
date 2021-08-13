using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.Requests;
using lestoma.CommonUtils.Responses;
using lestoma.Data;
using lestoma.Data.DAO;
using lestoma.Entidades.Models;
using lestoma.Logica.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace lestoma.Logica.LogicaService
{
    public class LSUsuario : IUsuarioService
    {
        private readonly Response _respuesta = new();
        private readonly Mapeo _db;

        private IGenericRepository<EUsuario> _usuarioRepository;
        public LSUsuario(IGenericRepository<EUsuario> usuarioRepository, Mapeo db)
        {
            _db = db;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Response> Login(LoginRequest login, string ip)
        {
            var us = await _db.TablaUsuarios.FirstOrDefaultAsync(x => x.Email.Equals(login.Email));

            var user = await new DAOUsuario().Logeo(login, _db);

            if (user == null)
            {
                _respuesta.Mensaje = "correo y/o contraseña incorrectos.";
            }
            else
            {
                if (HashHelper.CheckHash(login.Clave, user.Clave, user.Salt))
                {
                    _respuesta.Mensaje = "Ha iniciado satisfactoriamente.";
                    var refreshToken = generateRefreshToken(login.TipoAplicacion, user.Id, ip);
                    user.RefreshToken = refreshToken.Token;
                    _respuesta.Data = user;
                    user.RefreshTokens.Add(refreshToken);
                    await _usuarioRepository.Update(user);
                    _respuesta.IsExito = true;
                }
                else
                {
                    _respuesta.Mensaje = "correo y/o contraseña incorrectos.";
                }
            }
            return _respuesta;
        }

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

        public async Task<Response> Register(EUsuario usuario)
        {
            EUsuario existe = await new DAOUsuario().ExisteCorreo(usuario.Email, _db);
            if (existe != null)
            {
                _respuesta.Mensaje = "El correo ya esta en uso.";
            }
            else
            {
                var hash = HashHelper.Hash(usuario.Clave);
                usuario.Apellido = usuario.Apellido.Trim();
                usuario.Nombre = usuario.Nombre.Trim();
                usuario.RolId = (int)TipoRol.Auxiliar;
                usuario.Clave = hash.Password;
                usuario.Salt = hash.Salt;
                usuario.EstadoId = (int)TipoEstadoUsuario.CheckCuenta;
                await _usuarioRepository.Create(usuario);
                _respuesta.Mensaje = "Se ha registrado satisfactoriamente.";
                _respuesta.IsExito = true;
            }
            return _respuesta;
        }

        public async Task<Response> ChangePassword(ChangePasswordRequest cambiar)
        {
            var user = await _usuarioRepository.GetByIdAsync(cambiar.IdUser);
            if (user == null || !HashHelper.CheckHash(cambiar.OldPassword, user.Clave, user.Salt))
            {
                _respuesta.Mensaje = "Verifique la contraseña actual.";
            }
            else
            {
                var hash = HashHelper.Hash(cambiar.NewPassword);
                user.Clave = hash.Password;
                user.Salt = hash.Salt;
                await _usuarioRepository.Update(user);
                _respuesta.Mensaje = "Se actualizo satisfactoriamente.";
                _respuesta.IsExito = true;
            }
            return _respuesta;
        }


        public async Task<Response> lista()
        {

            _respuesta.Data = await _usuarioRepository.GetAll();
            return _respuesta;
        }

        public async Task<Response> ForgotPassword(ForgotPasswordRequest email)
        {
            var user = await new DAOUsuario().ExisteCorreo(email.Email, _db);
            if (user == null)
            {
                _respuesta.Mensaje = "El correo no esta registrado.";
            }

            else
            {
                bool validar;
                do
                {
                    user.CodigoRecuperacion = Reutilizables.generarCodigoVerificacion();
                    validar = await new DAOUsuario().ExisteCodigoVerificacion(user.CodigoRecuperacion, _db);
                } while (validar != false);
                user.FechaVencimientoCodigo = DateTime.Now.AddHours(2);
                await _usuarioRepository.Update(user);
                _respuesta.Data = user;
                _respuesta.IsExito = true;
                _respuesta.Mensaje = "Revise su correo eléctronico";
            }
            return _respuesta;
        }

        public async Task<Response> RecoverPassword(RecoverPasswordRequest recover)
        {
            var user = await new DAOUsuario().UsuarioByCodigoVerificacion(recover.Codigo, _db);
            if (user == null)
            {
                _respuesta.Mensaje = "codigo no valido.";
            }
            else
            {
                var hash = HashHelper.Hash(recover.Password);
                user.CodigoRecuperacion = null;
                user.Clave = hash.Password;
                user.Salt = hash.Salt;
                user.FechaVencimientoCodigo = null;
                await _usuarioRepository.Update(user);
                _respuesta.IsExito = true;
                _respuesta.Mensaje = "la contraseña ha sido restablecida.";
            }
            return _respuesta;
        }

        public Task<Response> ChangeProfile(ChangeProfileRequest change)
        {
            throw new NotImplementedException();
        }

        public bool RevokeToken(string token, string ipAddress)
        {
            throw new NotImplementedException();
        }

        public async Task<EUsuario> RefreshToken(string token, string ipAddress)
        {
            var user = _db.TablaUsuarios.Include(o => o.Rol).SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

            // return null if no user found with token
            if (user == null) return null;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return null if token is no longer active
            if (!refreshToken.IsActive) return null;

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

        public short GetExpiracionToken(int aplicacionId)
        {
            return _db.TablaAplicaciones.FirstOrDefault(x => x.Id == aplicacionId).TiempoExpiracionToken;
        }
    }
}
