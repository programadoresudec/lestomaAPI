using lestoma.CommonUtils.Responses;
using System.Threading.Tasks;

namespace lestoma.CommonUtils.Interfaces
{
    public interface IApiService
    {
        Task<Response> GetListAsyncWithToken<T>(string urlBase, string controller, string token, bool isLogin);
        Task<Response> PostAsync<T>(string urlBase, string controller, T model);
        Task<Response> PostAsyncWithToken<T>(string urlBase, string controller, T model, string token, bool isLogin);
    }
}
