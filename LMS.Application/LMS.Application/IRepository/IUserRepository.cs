using LMS.Domain.Entities;

namespace LMS.Application.IRepository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        void Update(User updatedUser);
    }
}
