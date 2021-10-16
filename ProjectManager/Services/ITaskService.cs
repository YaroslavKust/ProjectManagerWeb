using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectManager.UI.Models;

namespace ProjectManager.UI.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<MyTask>> GetAllAsync(int projectId);
        Task<MyTask> GetSingleAsync(int projectId, int id);
        Task<bool> CreateAsync(MyTask task);
        Task<bool> UpdateAsync(MyTask task);
        Task<bool> DeleteAsync(int projectId, int id);
    }
}