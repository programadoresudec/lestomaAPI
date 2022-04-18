using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Requests;
using lestoma.Entidades.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace lestoma.Logica.Interfaces
{
    public interface IUsuarioService
    {
        Task<Response> Login(LoginRequest login, string ip);
        Task<Response> Register(EUsuario usuario);
        Task<Response> ChangePassword(ChangePasswordRequest usuario);
        Task<Response> ForgotPassword(ForgotPasswordRequest email);
        Task<Response> RecoverPassword(RecoverPasswordRequest recover);
        Task<Response> ChangeProfile(ChangeProfileRequest change);
        bool RevokeToken(string token, string ipAddress);
        Task<EUsuario> RefreshToken(string refreshToken, string ipAddress);
        List<UserDTO> GetUsersJustNames();
        short GetExpirationToken(int aplicacionId);
        Task<string> GetApplicationType(int tipoAplicacion);
        Task<Response> EditRol(RolRequest user);
        Task<Response> GetByIdAsync(int id);
    }
}
