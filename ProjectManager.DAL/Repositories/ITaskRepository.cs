using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectManager.DAL.Models;
using ProjectManager.Entities.Models;

namespace ProjectManager.DAL.Repositories
{
    public interface ITaskRepository: IRepository<MyTask>
    {
        Task<IEnumerable<MyTask>> GetTasksAsync(int projectId);

        Task<MyTask> GetTaskAsync(int projectId, int id);
    }
}