using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Entities.DTO
{
    public class TaskForCreate
    {
        [Required]
        public string Description { get; set; }
    }
}