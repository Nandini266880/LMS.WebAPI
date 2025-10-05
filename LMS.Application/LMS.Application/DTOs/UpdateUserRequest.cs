using System.ComponentModel.DataAnnotations;

namespace LMS.Application.DTOs
{

    public class UpdateUserRequest
    {
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string Email { get; set; }

        public string? Password { get; set; } 

        [Required]
        public string Role { get; set; } 
    }

}
