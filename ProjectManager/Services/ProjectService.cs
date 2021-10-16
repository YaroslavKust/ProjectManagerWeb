using ProjectManager.UI.Common;
using ProjectManager.UI.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjectManager.UI.Services
{
    public class ProjectService: IProjectService
    {
        private IConnector _connector;
        private readonly string _projectUrl;

        public ProjectService(IConnector connector)
        {
            _connector = connector;
            _projectUrl = ConfigurationManager.AppSettings["BaseUrl"] + "/projects";
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            var response = await _connector.SendGetAsync(_projectUrl);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<Project>>();
            }

            return null;
        }

        public async Task<Project> GetSingleAsync(int id)
        {
            string url = _projectUrl + $"/{id}";
            var response = await _connector.SendGetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<Project>();
            }

            return null;
        }

        public async Task<bool> CreateAsync(Project project)
        {
            var response = await _connector.SendPostAsync<Project>(_projectUrl,project);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateAsync(Project project)
        {
            var url = _projectUrl + $"/{project.Id}";

            var response = await _connector.SendPutAsync<Project>(url, project);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            string url = _projectUrl + $"/{id}";
            var response = await _connector.SendDeleteAsync(url);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }
    }
}