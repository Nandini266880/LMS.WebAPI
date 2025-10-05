using LMS.Application.IRepository;
using LMS.Domain.Entities;
using LMS.Infrastructure.Data;

namespace LMS.Infrastructure.Repository
{
    public class LessonRepository : BaseRepository<Lesson>, ILessonRepository
    {
        private readonly AppDBContext _db;
        public LessonRepository(AppDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Lesson updatedLesson)
        {
            _db.Update(updatedLesson);
        }
    }
}
