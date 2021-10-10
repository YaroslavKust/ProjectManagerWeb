using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ProjectManager.Entities.Models;

namespace ProjectManager.DAL.Repositories
{
    public interface IProjectRepository: IRepository<Project>
    {
        Task<IEnumerable<Project>> GetProjectsAsync(Expression<Func<Project, bool>> exp);
        Task<Project> GetProjectAsync(int id);
    }
}
