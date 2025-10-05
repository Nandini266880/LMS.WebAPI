using LMS.Application.DTOs;

namespace LMS.Application.Services.IServices
{
    public interface IExamAttemptService
    {
        Task<ExamAttemptDto> AddExamAttemptAsync(int userId, int examId, List<UserAnswerDto> userAnswers);
        Task<ExamAttemptDto> GetExamAttemptById(int attemptId);
        Task<IEnumerable<ExamAttemptDto>> GetExamAttemptsOfUser(int userId);
    }
}
