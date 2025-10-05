using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Domain.Entities
{
    public class ExamAttempt
    {
        [Key]
        public int AttemptId { get; set; }
        [Required]
        public int ExamId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int Score { get; set; } 
        public DateTime AttemptedAt { get; set; } = DateTime.UtcNow;

        //Navigation Property
        [ForeignKey("UserId")]
        public User? User { get; set; }

        [ForeignKey("ExamId")]
        public Exam? Exam { get; set; }
    }
}
