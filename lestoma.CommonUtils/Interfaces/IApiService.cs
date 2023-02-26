using lestoma.CommonUtils.DTOs;
using System.Threading.Tasks;

namespace lestoma.CommonUtils.Interfaces
{
    public interface IApiService
    {
        Task<ResponseDTO> GetPaginadoAsyncWithToken<T>(string urlBase, string nameService, string token);
        Task<ResponseDTO> GetListAsyncWithToken<T>(string urlBase, string nameService, string token);
        Task<ResponseDTO> GetAsyncWithToken(string urlBase, string nameService, string token);
        Task<ResponseDTO> PostAsync<T>(string urlBase, string nameService, T model);
        Task<ResponseDTO> PutAsync<T>(string urlBase, string nameService, T model);
        Task<ResponseDTO> PostAsyncWithToken<T>(string urlBase, string nameService, T model, string token);
        Task<ResponseDTO> PostWithoutBodyAsyncWithToken(string urlBase, string nameService, string token);
        Task<ResponseDTO> PutAsyncWithToken<T>(string urlBase, string nameService, T model, string token);
        Task<ResponseDTO> DeleteAsyncWithToken(string urlBase, string nameService, object id, string token);
        bool CheckConnection();

    }
}
