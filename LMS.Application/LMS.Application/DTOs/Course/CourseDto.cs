using LMS.Domain.Entities;
using LMS.WebAPI.Controllers;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LMS.Application.DTOs.Course
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ThumbnailUrl { get; set; } = string.Empty;
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<LessonDto> Lessons { get; set; } = new List<LessonDto>();
    }
    public class CourseCreateDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(10)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string ThumbnailUrl { get; set; } = string.Empty;

        public double Price { get; set; } = 0;
    }

}
