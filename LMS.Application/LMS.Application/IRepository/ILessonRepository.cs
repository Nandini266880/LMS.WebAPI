using LMS.Domain.Entities;

namespace LMS.Application.IRepository
{
    public interface ILessonRepository : IBaseRepository<Lesson>
    {
        void Update(Lesson updatedLesson);
    }
}
