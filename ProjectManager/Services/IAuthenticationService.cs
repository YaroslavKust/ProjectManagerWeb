using System.Threading.Tasks;
using ProjectManager.UI.Models;

namespace ProjectManager.UI.Services
{
    public interface IAuthenticationService
    {
        Task AuthorizeAsync(string name, string password);
        Task RegisterAsync(string name, string password);
    }
}