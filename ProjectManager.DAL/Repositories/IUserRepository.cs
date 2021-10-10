using System.Threading.Tasks;
using ProjectManager.Entities.Models;

namespace ProjectManager.DAL.Repositories
{
    public interface IUserRepository: IRepository<User>
    {
        Task<User> GetUserAsync(string name, string password);
    }
}