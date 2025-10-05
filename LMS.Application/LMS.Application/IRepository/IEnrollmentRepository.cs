using LMS.Domain.Entities;

namespace LMS.Application.IRepository
{
    public interface IEnrollmentRepository : IBaseRepository<Enrollment>
    {
        Task<bool> HasEnrollAsync(int userId, int courseId);
        void Update(Enrollment enrollment);
    }
}
