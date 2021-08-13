using lestoma.CommonUtils.Requests;
using lestoma.CommonUtils.Responses;
using lestoma.Entidades.Models;
using System.Threading.Tasks;

namespace lestoma.Logica.Interfaces
{
    public interface IUsuarioService
    {
        Task<Response> Login(LoginRequest login, string ip);
        Task<Response> Register(EUsuario usuario);
        Task<Response> ChangePassword(ChangePasswordRequest usuario);
        Task<Response> lista();
        Task<Response> ForgotPassword(ForgotPasswordRequest email);
        Task<Response> RecoverPassword(RecoverPasswordRequest recover);
        Task<Response> ChangeProfile(ChangeProfileRequest change);
        bool RevokeToken(string token, string ipAddress);
        Task<EUsuario> RefreshToken(string refreshToken, string ipAddress);
        short GetExpiracionToken(int aplicacionId);
    }
}
