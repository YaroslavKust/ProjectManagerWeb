using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectManager.UI.Models;

namespace ProjectManager.UI.Services
{
    public interface IProjectService: IDataService<Project>
    {
        Task<IEnumerable<Project>> GetAllAsync();
        Task<Project> GetSingleAsync(int id);
        Task<bool> CreateAsync(Project project);
        Task<bool> UpdateAsync(Project project);
        Task<bool> DeleteAsync(int id);
    }
}