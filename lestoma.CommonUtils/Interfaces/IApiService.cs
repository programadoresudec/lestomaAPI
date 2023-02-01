using lestoma.CommonUtils.DTOs;
using lestoma.CommonUtils.Helpers;
using System;
using System.Threading.Tasks;

namespace lestoma.CommonUtils.Interfaces
{
    public interface IApiService
    {
        Task<ResponseDTO> GetPaginadoAsyncWithToken<T>(string urlBase, string controller, string token);
        Task<ResponseDTO> GetListAsyncWithToken<T>(string urlBase, string controller, string token);
        Task<ResponseDTO> GetByIdAsyncWithToken<T>(string urlBase, string controller, string token);
        Task<ResponseDTO> PostAsync<T>(string urlBase, string controller, T model);
        Task<ResponseDTO> PutAsync<T>(string urlBase, string controller, T model);
        Task<ResponseDTO> DeleteAsyncWithToken(string urlBase, string controller, object id, string token);
        Task<ResponseDTO> PostAsyncWithToken<T>(string urlBase, string controller, T model, string token);
        Task<ResponseDTO> PutAsyncWithToken<T>(string urlBase, string controller, T model, string token);
        bool CheckConnection();
    }
}
