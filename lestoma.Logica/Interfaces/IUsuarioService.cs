using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Requests;
using lestoma.Entidades.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lestoma.Logica.Interfaces
{
    public interface IUsuarioService
    {
        Task<ResponseDTO> Login(LoginRequest login);
        Task<ResponseDTO> RegisterUser(EUsuario usuario, bool ownRegister = true);
        Task<ResponseDTO> UpdateUser(EUsuario usuario);
        Task<ResponseDTO> ChangePassword(ChangePasswordRequest usuario);
        Task<ResponseDTO> ForgotPassword(ForgotPasswordRequest email);
        Task<ResponseDTO> RecoverPassword(RecoverPasswordRequest recover);
        Task<ResponseDTO> ChangeProfile(ChangeProfileRequest change);
        Task<EUsuario> RefreshToken(string refreshToken, string ipAddress);
        List<UserDTO> GetUsersJustNames(bool isSuperAdmin);
        short GetExpirationToken(int aplicacionId);
        Task<string> GetApplicationType(int tipoAplicacion);
        Task<ResponseDTO> EditRol(RolRequest user);
        Task<ResponseDTO> RevokeToken(string token, string ipAddress);
        Task<ResponseDTO> GetByIdAsync(int id);
        Task<IEnumerable<InfoUserDTO>> GetInfoUsers();
        Task<IEnumerable<EstadoDTO>> GetUserStatuses();
        Task<IEnumerable<RolDTO>> GetUserRoles();
        Task<ResponseDTO> ActivateNotificationsMail(string email);
        Task<ResponseDTO> DesactivateNotificationsMail(string email);
        Task<ResponseDTO> UserIsActiveWithNotificationsMail(string email);
    }
}
