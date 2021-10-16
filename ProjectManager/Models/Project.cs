using System.Collections.ObjectModel;

namespace ProjectManager.UI.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ObservableCollection<MyTask> Tasks { get; set; }
    }
}