using LMS.Application.IRepository;
using LMS.Domain.Entities;
using LMS.Infrastructure.Data;

namespace LMS.Infrastructure.Repository
{
    public class ProgressRepository : BaseRepository<Progress>, IProgressRepository
    {
        private readonly AppDBContext _db;

        public ProgressRepository(AppDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Progress progress)
        {
            _db.Update(progress);
        }
    }

}
