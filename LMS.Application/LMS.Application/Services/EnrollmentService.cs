using LMS.Application.IRepository;
using LMS.Application.Services.IServices;
using LMS.Domain.Entities;
using LMS.Domain.Enums;

namespace LMS.Application.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EnrollmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Enrollment> GetEnrollmentById(int id)
        {
            var enroll = await _unitOfWork.Enrollments.GetAsync(u => u.Id == id);
            if(enroll==null)
            {
                throw new InvalidOperationException("Enrollment doesn't exist.");
            }
            return enroll;
        }

        public async Task EnrollStudentAsync(int userId, int courseId)
        {
            var existing = _unitOfWork.Enrollments.GetAsync(u=>u.UserId == userId 
                && u.CourseId == courseId && u.Status != EnrollStatus.Cancelled);

            if (existing != null)
            {
                throw new InvalidOperationException("Student is already enrolled in the course.");
            }

            var enrollment = new Enrollment
            {
                UserId = userId,
                CourseId = courseId,
                Status = EnrollStatus.Active,
                EnrolledAt = DateTime.UtcNow
            };

            await _unitOfWork.Enrollments.AddAsync(enrollment);
        }
        public async Task<bool> HasEnrolledInCourse(int userId, int courseId)
        {
            return await _unitOfWork.Enrollments.HasEnrollAsync(userId, courseId); 
        }

        public async Task UpdateEnrollmentStatusAsync(int enrollmentId, EnrollStatus newStatus)
        {
            var enrollment = await _unitOfWork.Enrollments.GetAsync(e => e.Id == enrollmentId);
            if (enrollment == null)
                throw new KeyNotFoundException("Enrollment not found.");

            if (enrollment.Status != EnrollStatus.Active)
            {
                throw new InvalidOperationException("Only active enrollments can be updated.");
            }

            if (newStatus != EnrollStatus.Completed && newStatus != EnrollStatus.Cancelled)
            {
                throw new InvalidOperationException("Invalid status update.");
            }

            enrollment.Status = newStatus;
            _unitOfWork.Enrollments.Update(enrollment);
        }


    }
}
