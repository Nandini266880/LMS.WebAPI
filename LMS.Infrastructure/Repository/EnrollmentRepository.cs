using LMS.Application.IRepository;
using LMS.Domain.Entities;
using LMS.Domain.Enums;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Repository
{
    public class EnrollmentRepository : BaseRepository<Enrollment>, IEnrollmentRepository
    {
        private readonly AppDBContext _db;
        public EnrollmentRepository(AppDBContext db) : base(db) 
        {
            _db = db;
        }

        public async Task<bool> HasEnrollAsync(int userId, int courseId)
        {
            bool hasAccess = await _db.Enrollments.AnyAsync(e =>
                e.UserId == userId &&
                e.CourseId == courseId &&
                e.Status == EnrollStatus.Active
            );
            return hasAccess;
        }

        public void Update(Enrollment enrollment)
        {
            _db.Update(enrollment);
        }

    }
}
