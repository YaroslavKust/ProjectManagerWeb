namespace ProjectManager.Entities.Models
{
    public class MyTask
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int ProgressInPercents { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}