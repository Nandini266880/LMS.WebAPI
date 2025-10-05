using System.ComponentModel.DataAnnotations;

namespace LMS.Application.DTOs
{
    public class PaymentRequestDto
    {
        [Required]
        public int CourseId { get; set; }
        [Required]
        public double Amount { get; set; }
    }
}
