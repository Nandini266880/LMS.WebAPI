using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.DTOs
{
    public class StudentCourseProgressDto
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int TotalLessons { get; set; }
        public int CompletedLessons { get; set; }
        public int CompletionPercentage { get; set; }
    }

}
