using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ProjectManager.Entities.Models;

namespace ProjectManager.DAL.Repositories
{
    class ProjectRepository: Repository<Project>, IProjectRepository
    {
        public ProjectRepository(DbContext context): base(context) { }
        public async Task<IEnumerable<Project>> GetProjectsAsync(Expression<Func<Project, bool>> exp)
        {
            return await Get(exp).ToListAsync();
        }

        public async Task<Project> GetProjectAsync(int userId, int id)
        {
            return await Get(p => p.UserId == userId && p.Id == id).FirstOrDefaultAsync();
        }
    }
}
