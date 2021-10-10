using System.Threading.Tasks;
using ProjectManager.Entities.DTO;

namespace ProjectManager.API.Services.Authentication
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserForAuth user);
        string GenerateToken();
        Task<bool> CreateUser(UserForAuth user);
    }
}