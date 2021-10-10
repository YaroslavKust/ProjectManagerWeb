using System.Collections.Generic;

namespace ProjectManager.Entities.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public IEnumerable<Project> Projects { get; set; }
    }
}