using LMS.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Domain.Entities
{
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]

        [Required]
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
        [Required]
        public EnrollStatus Status { get; set; } = EnrollStatus.Active;

        //Navigation
        public User? User { get; set; }
        public Course? Course { get; set; }
    }
}
