using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Entities.DTO
{
    public class UserForAuth
    {
        [Required]
        [MinLength(4)]
        public string Name { get; set; }
        [Required]
        [MinLength(4)]
        public string Password { get; set; }
    }
}