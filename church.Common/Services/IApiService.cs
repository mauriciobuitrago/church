using Church.Common.Models;
using Church.Common.Requests;
using Church.Common.Responses;
using System.IO;
using System.Threading.Tasks;

namespace Church.Common.Services
{
    public interface IApiService
    {
        Task<RandomUsers> GetRandomUser(string urlBase, string servicePrefix);

        Task<Stream> GetPictureAsync(string urlBase, string servicePrefix);

        Task<Response> GetListAsync<T>(string urlBase, string servicePrefix, string controller);

        Task<Response> GetTokenAsync(string urlBase, string servicePrefix, string controller, TokenRequest request);


    }
}