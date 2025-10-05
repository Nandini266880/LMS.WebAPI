using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LMS.Domain.Entities
{
    public class Lesson
    {
        [Key]
        public int Id { get; set; }
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        [JsonIgnore]
        public Course? Course { get; set; }
        [Required]
        public string Title { get; set; }
        public string ContentUrl { get; set; }
        [Required]
        public int Duration { get; set; }  
        public int LessonIdx { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Progress> Progresses { get; } = new List<Progress>();

    }
}
