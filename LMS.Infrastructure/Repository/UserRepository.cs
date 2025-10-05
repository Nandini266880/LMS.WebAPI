using LMS.Application.IRepository;
using LMS.Domain.Entities;
using LMS.Infrastructure.Data;

namespace LMS.Infrastructure.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly AppDBContext _db;
        public UserRepository(AppDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(User updatedUser)
        {
            _db.Update(updatedUser);
        }
    }
}
