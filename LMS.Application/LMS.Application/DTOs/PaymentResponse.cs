using LMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.DTOs
{
    public class PaymentResponseDto
    {
        public int PaymentId { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentStatus Status { get; set; }
    }

}
