using LMS.Application.IRepository;
using LMS.Domain.Entities;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repository
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        private readonly AppDBContext _db;

        public CourseRepository(AppDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Course updatedCourse)
        {
            _db.Update(updatedCourse);
        }
    }

}
