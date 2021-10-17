using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Entities.DTO
{
    public class TaskForUpdate
    {
        [Required]
        public string Description { get; set; }
        [Range(0,100)]
        public int ProgressInPercents { get; set; }
    }
}