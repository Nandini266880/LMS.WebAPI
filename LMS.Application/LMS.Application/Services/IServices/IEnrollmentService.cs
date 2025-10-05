using LMS.Domain.Entities;
using LMS.Domain.Enums;

namespace LMS.Application.Services.IServices
{
    public interface IEnrollmentService 
    {
        Task<bool> HasEnrolledInCourse(int userId, int courseId);
        Task<Enrollment> GetEnrollmentById(int id);
        Task EnrollStudentAsync(int userId, int courseId);
        Task UpdateEnrollmentStatusAsync(int enrollmentId, EnrollStatus newStatus);
    }
}
