using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LMS.Domain.Entities
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(10)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string ThumbnailUrl { get; set; } = string.Empty;

        [Required]
        public double Price { get; set; } = 0;

        public int CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        [JsonIgnore]
        public User? User { get; set; }  

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        [JsonIgnore]
        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

        [JsonIgnore]
        public ICollection<Exam> Exams { get; } = new List<Exam>();
        [JsonIgnore]
        public ICollection<Enrollment> Enrollments { get; } = new List<Enrollment>();
    }
}
