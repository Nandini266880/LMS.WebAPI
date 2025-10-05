using LMS.Domain.Entities;

namespace LMS.Application.IRepository
{
    public interface IProgressRepository : IBaseRepository<Progress>
    {
        void Update(Progress progress);
    }
}
