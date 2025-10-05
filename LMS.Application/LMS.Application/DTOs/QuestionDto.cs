using System.ComponentModel.DataAnnotations;

namespace LMS.Application.DTOs
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public int Marks { get; set; }

        public List<OptionDto> Options { get; set; } = new List<OptionDto>();
    }

    public class OptionDto
    {
        public int Id { get; set; }
        public string OptionText { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }

    public class QuestionCreateDto
    {
        [Required]
        public int ExamId { get; set; }

        [Required]
        public string QuestionText { get; set; } = string.Empty;

        [Required]
        public int Marks { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "A question must have at least 2 options.")]
        public List<OptionCreateDto> Options { get; set; } = new List<OptionCreateDto>();
    }
    public class OptionCreateDto
    {
        [Required]
        public string OptionText { get; set; } = string.Empty;

        [Required]
        public bool IsCorrect { get; set; }
    }

}
