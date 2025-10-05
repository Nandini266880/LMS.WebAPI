using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LMS.Domain.Entities
{
    public class Progress
    {
        [Key]
        public int ProgressId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int LessonId { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DateTime? CompletedAt { get; set; }

        //Navigation
        [JsonIgnore]
        [ForeignKey("UserId")]
        public User? User { get; set; }
        [JsonIgnore]
        [ForeignKey("LessonId")]
        public Lesson? Lesson { get; set; }
    }
}
