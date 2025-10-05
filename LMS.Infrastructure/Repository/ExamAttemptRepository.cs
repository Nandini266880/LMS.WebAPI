using LMS.Application.IRepository;
using LMS.Domain.Entities;
using LMS.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repository
{
    public class ExamAttemptRepository : BaseRepository<ExamAttempt>, IExamAttemptRepository
    {
        private readonly AppDBContext _db;

        public ExamAttemptRepository(AppDBContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<ExamAttempt>> GetAttemptsByUserAsync(int userId)
        {
            var userIdParam = new SqlParameter("@UserId", userId);

            return await _db.ExamAttempts
                .FromSqlRaw("EXEC GetExamAttemptsByUser @UserId", userIdParam)
                .ToListAsync();
        }

        public async Task<bool> UserExistsAsync(int userId)
        {
            return await _db.ExamAttempts.AnyAsync(e => e.UserId == userId);
        }

        public void Update(ExamAttempt updatedExam)
        {
            _db.Update(updatedExam);
        }
    }

}
