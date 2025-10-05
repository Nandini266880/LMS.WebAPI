using LMS.Domain.Entities;

namespace LMS.Application.IRepository
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
        void Update(Course updatedCourse);
    }
}
