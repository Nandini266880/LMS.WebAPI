using LMS.Application.IRepository;
using LMS.Domain.Entities;
using LMS.Infrastructure.Data;

namespace LMS.Infrastructure.Repository
{
    public class ExamRepository : BaseRepository<Exam>, IExamRepository
    {
        private readonly AppDBContext _db;

        public ExamRepository(AppDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Exam updatedExam)
        {
            _db.Update(updatedExam);
        }
    }

}
