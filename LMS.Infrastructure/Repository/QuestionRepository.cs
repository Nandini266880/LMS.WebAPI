using LMS.Application.IRepository;
using LMS.Domain.Entities;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repository
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        private readonly AppDBContext _db;

        public QuestionRepository(AppDBContext db) : base(db)
        {
            _db = db;
        }
    }

}
