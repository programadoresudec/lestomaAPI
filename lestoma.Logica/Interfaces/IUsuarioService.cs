using lestoma.CommonUtils.Entities;
using lestoma.CommonUtils.Requests;
using lestoma.CommonUtils.Responses;
using System.Threading.Tasks;

namespace lestoma.Logica.Interfaces
{
    public interface IUsuarioService
    {
        Task<Response> Login(LoginRequest login);
        Task<Response> Register(EUsuario usuario);
        Task<Response> ChangePassword(ChangePasswordRequest usuario);
        Task<Response> lista();
        Task<Response> ForgotPassword(ForgotPasswordRequest email);
        Task<Response> RecoverPassword(RecoverPasswordRequest recover);
        Task<Response> ChangeProfile(ChangeProfileRequest change);
    }
}
