using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ProjectManager.UI.Common
{
    public class JsonConnector: IConnector
    {
        private readonly HttpClient _client = new HttpClient();

        public async Task<HttpResponseMessage> SendGetAsync(
            string request, bool useAuthorization = true)
        {
            ConfigureAuthorization(useAuthorization);
            return await _client.GetAsync(request);
        }


        public async Task<HttpResponseMessage> SendPostAsync<T>(
            string request,T content, bool useAuthorization = true)
        {
            ConfigureAuthorization(useAuthorization);
            return await _client.PostAsJsonAsync(request, content); ;
        }


        public async Task<HttpResponseMessage> SendPutAsync<T>(
            string request, T content, bool useAuthorization = true)
        {
            ConfigureAuthorization(useAuthorization);
            return await _client.PutAsJsonAsync(request, content);
        }


        public async Task<HttpResponseMessage> SendDeleteAsync(
            string request, bool useAuthorization = true)
        {
            ConfigureAuthorization(useAuthorization);
            return await _client.DeleteAsync(request);
        }


        private void ConfigureAuthorization(bool useAuth)
        {
            _client.DefaultRequestHeaders.Authorization = 
                useAuth ? new AuthenticationHeaderValue("Bearer", App.Token) : null;
        }
            
    }
}