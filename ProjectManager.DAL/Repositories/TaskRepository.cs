using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ProjectManager.DAL.Models;
using ProjectManager.Entities.Models;

namespace ProjectManager.DAL.Repositories
{
    public class TaskRepository:Repository<MyTask>, ITaskRepository
    {
        public TaskRepository(DbContext context):base(context){ }
        public async Task<IEnumerable<MyTask>> GetTasksAsync(int projectId)
        {
            return await Get(t => t.ProjectId == projectId).ToListAsync();
        }

        public async Task<MyTask> GetTaskAsync(int projectId, int id)
        {
            return await Get(t => t.ProjectId == projectId && t.Id == id).FirstOrDefaultAsync();
        }
    }
}