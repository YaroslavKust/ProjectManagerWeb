using System.Collections.Generic;

namespace ProjectManager.Entities.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public  IEnumerable<MyTask> Tasks { get; set; }
    }
}