using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Helpers;
using System;
using System.Threading.Tasks;

namespace lestoma.CommonUtils.Interfaces
{
    public interface IApiService
    {
        Task<Response> GetPaginadoAsyncWithToken<T>(string urlBase, string controller, string token);
        Task<Response> GetListAsyncWithToken<T>(string urlBase, string controller, string token);
        Task<Response> PostAsync<T>(string urlBase, string controller, T model);
        Task<Response> PutAsync<T>(string urlBase, string controller, T model);
        Task<Response> DeleteAsyncWithToken(string urlBase, string controller, object id, string token);
        Task<Response> PostAsyncWithToken<T>(string urlBase, string controller, T model, string token);
        Task<Response> PutAsyncWithToken<T>(string urlBase, string controller, T model, string token);
        bool CheckConnection();
    }
}
