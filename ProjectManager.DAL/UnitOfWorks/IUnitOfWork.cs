using System;
using System.Threading.Tasks;
using ProjectManager.DAL.Models;
using ProjectManager.DAL.Repositories;

namespace ProjectManager.DAL.UnitOfWorks
{
    public interface IUnitOfWork: IDisposable
    {
        IUserRepository Users { get; }
        IProjectRepository Projects { get; }
        ITaskRepository Tasks { get; }
        Task<int> SaveAsync();
    }
}
