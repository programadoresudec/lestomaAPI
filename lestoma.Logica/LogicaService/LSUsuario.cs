using lestoma.CommonUtils.Entities;
using lestoma.CommonUtils.Enums;
using lestoma.CommonUtils.Helpers;
using lestoma.CommonUtils.Requests;
using lestoma.CommonUtils.Responses;
using lestoma.Data;
using lestoma.Data.DAO;
using lestoma.Logica.Interfaces;
using System;
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

        public async Task<Response> Login(LoginRequest login)
        {
            var user = await new DAOUsuario().Logeo(login, _db);
            if (user == null)
            {
                _respuesta.Mensaje = "correo y/o contraseña incorrectos.";
            }
            else
            {
                _respuesta.Mensaje = "Ha iniciado satisfactoriamente.";
                _respuesta.Data = user;
                _respuesta.IsExito = true;
            }
            return _respuesta;
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
                usuario.Apellido = usuario.Apellido.Trim();
                usuario.Nombre = usuario.Nombre.Trim();
                usuario.RolId = (int)TipoRol.Auxiliar;
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
            if (user == null || !user.Clave.Equals(cambiar.OldPassword))
            {
                _respuesta.Mensaje = "Verifique la contraseña actual.";
            }
            else
            {
                user.Clave = cambiar.NewPassword;
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
                user.CodigoRecuperacion = null;
                user.Clave = recover.Password;
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
    }
}
