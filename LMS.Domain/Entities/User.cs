using LMS.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LMS.Domain.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]

        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        //Navigation
        [JsonIgnore]
        public ICollection<Course> Courses { get; } = new List<Course>();      
        [JsonIgnore]
        public ICollection<Enrollment> Enrollments { get; } = new List<Enrollment>(); 
        [JsonIgnore]
        public ICollection<ExamAttempt> ExamAttempts { get; } = new List<ExamAttempt>();
        [JsonIgnore]
        public ICollection<Progress> Progresses { get; } = new List<Progress>();

        //Token Service - Storing in user table
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
