using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LMS.WebAPI.Controllers
{
    public class LessonDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        public int Duration { get; set; }
        public int LessonIdx { get; set; }
    }
    public class LessonCreateDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public IFormFile File { get; set; } 
        [Required]
        public int Duration { get; set; }
    }
}