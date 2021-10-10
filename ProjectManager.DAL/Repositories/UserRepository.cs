using System.Data.Entity;
using System.Threading.Tasks;
using ProjectManager.Entities.Models;

namespace ProjectManager.DAL.Repositories
{
    public class UserRepository: Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context){ }

        public async Task<User> GetUserAsync(string name, string password)
        {
            return await Get(u=>u.Name == name && u.Password == password).FirstOrDefaultAsync();
        }
    }
}