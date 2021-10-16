using System.Net.Http;
using System.Threading.Tasks;

namespace ProjectManager.UI.Common
{
    public interface IConnector
    {
        Task<HttpResponseMessage> SendGetAsync(string request, bool useAuthorization = true);
        Task<HttpResponseMessage> SendPostAsync<T>(string request, T content, bool useAuthorization = true);
        Task<HttpResponseMessage> SendPutAsync<T>(string request, T content, bool useAuthorization = true);
        Task<HttpResponseMessage> SendDeleteAsync(string request, bool useAuthorization = true);
    }
}