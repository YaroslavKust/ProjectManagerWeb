using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManager.UI.Services
{
    public interface IDataService<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetSingleAsync(int id);
        Task<bool> CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}