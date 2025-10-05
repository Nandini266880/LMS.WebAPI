using LMS.Domain.Entities;

namespace LMS.Application.IRepository
{
    public interface IExamAttemptRepository : IBaseRepository<ExamAttempt>
    {
        Task<IEnumerable<ExamAttempt>> GetAttemptsByUserAsync(int userId);
        Task<bool> UserExistsAsync(int userId);

        void Update(ExamAttempt updatedExam);
    }
}
