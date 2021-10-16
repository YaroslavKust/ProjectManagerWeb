using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectManager.UI.Common;
using ProjectManager.UI.Models;

namespace ProjectManager.UI.Services
{
    public class AuthenticationService: IAuthenticationService
    {
        private IConnector _connector;
        private string _authUrl;

        public AuthenticationService(IConnector connector)
        {
            _connector = connector;
            _authUrl = ConfigurationManager.AppSettings["BaseUrl"] + "/authentication";
        }

        public async Task AuthorizeAsync(string name, string password)
        {
            var user = new User { Name = name, Password = password };
            var result = await _connector.SendPostAsync<User>(
                _authUrl + "/login", user, useAuthorization: false);

            if (result.IsSuccessStatusCode)
            {
                var jsonResult = await result.Content.ReadAsStringAsync();
                App.Token = JObject.Parse(jsonResult)["token"].ToString();
            }
            else throw new Exception();
        }


        public async Task RegisterAsync(string name, string password)
        {
            var user = new User { Name = name, Password = password };
            var result = await _connector.SendPostAsync<User>(
                _authUrl, user, useAuthorization: false);

            if (!result.IsSuccessStatusCode)
                throw new Exception();
        }
    }
}