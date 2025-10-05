namespace LMS.Application.DTOs
{
    // Response DTO
    public class ExamDto
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int TotalMarks { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    // For Creating Exam
    public class CreateExamDto
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int TotalMarks { get; set; }
    }

    public class ExamAttemptDto
    {
        public int UserId { get; set; }
        public int ExamId { get; set; }
        public int Score { get; set; }
        public DateTime AttemptedAt { get; set; }
    }

    // For Updating Exam
    public class UpdateExamDto
    {
        public string Title { get; set; } = string.Empty;   
        public int TotalMarks { get; set; }
    }
}

