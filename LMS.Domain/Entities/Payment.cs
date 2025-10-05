using LMS.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace LMS.Domain.Entities
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course? Course { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    }
}
