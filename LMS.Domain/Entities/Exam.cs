using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Domain.Entities
{
    public class Exam
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]

        [Required]
        [StringLength(200)]
        public string Title { get; set; }  = string.Empty;

        [Required]
        public int TotalMarks { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Course? Course { get; set; }
        public ICollection<Question> Questions { get; } = new List<Question>();
        public ICollection<ExamAttempt> ExamAttempts { get; } = new List<ExamAttempt>();

    }
}
