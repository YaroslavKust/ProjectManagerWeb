using System.Data.Entity;
using ProjectManager.Entities.Models;

namespace ProjectManager.DAL.Models
{
    public class ManagerContext: DbContext
    {
        public ManagerContext(string connection) : base(connection) 
        {
            // Database.SetInitializer<ManagerContext>
            //    (new MigrateDatabaseToLatestVersion<ManagerContext, Configuration>());
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<MyTask> Tasks { get; set; }
    }
}