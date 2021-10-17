using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Entities.DTO
{
    public class ProjectForManipulation
    {
        [Required]
        public string Name { get; set; }
    }
}