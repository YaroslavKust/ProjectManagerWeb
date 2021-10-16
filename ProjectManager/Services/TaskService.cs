using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.UI.Common;
using ProjectManager.UI.Models;

namespace ProjectManager.UI.Services
{
    public class TaskService: ITaskService
    {
        private IConnector _connector;

        public TaskService(IConnector connector)
        {
            _connector = connector;
        }

 
        public async Task<IEnumerable<MyTask>> GetAllAsync(int projectId)
        {
            var url = BuildTaskUrl(projectId);
            var result = await _connector.SendGetAsync(url);

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadAsAsync<IEnumerable<MyTask>>();
            }

            return null;
        }


        public async Task<MyTask> GetSingleAsync(int projectId, int id)
        {
            var url = BuildTaskUrl(projectId) + $"/{id}";
            var result = await _connector.SendGetAsync(url);

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadAsAsync<MyTask>();
            }

            return null;
        }


        public async Task<bool> CreateAsync(MyTask task)
        {
            var url = BuildTaskUrl(task.ProjectId);
            var response = await _connector.SendPostAsync<MyTask>(url, task);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                return true;
            }

            return false;
        }


        public async Task<bool> UpdateAsync(MyTask task)
        {
            var url = BuildTaskUrl(task.ProjectId) + $"/{task.Id}";
            var response = await _connector.SendPutAsync<MyTask>(url, task);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }


        public async Task<bool> DeleteAsync(int projectId, int id)
        {
            var url = BuildTaskUrl(projectId) + $"/{id}";
            var response = await _connector.SendDeleteAsync(url);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }


        private string BuildTaskUrl(int projectId)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(ConfigurationManager.AppSettings["BaseUrl"]);
            builder.Append("/projects");
            builder.Append($"/{projectId}");
            builder.Append("/tasks");

            return builder.ToString();
        }
    }
}