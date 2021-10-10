using System.Data.Entity.Infrastructure;

namespace ProjectManager.DAL.Models
{
    public class ManagerContextFactory: IDbContextFactory<ManagerContext>
    {
        public ManagerContext Create()
        {
            return new ManagerContext("data source=.\\SQLEXPRESS;Initial Catalog=managerdb;Integrated Security=true;MultipleActiveResultSets=true");
        }
    }
}