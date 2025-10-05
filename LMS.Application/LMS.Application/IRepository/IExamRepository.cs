using LMS.Domain.Entities;

namespace LMS.Application.IRepository
{
    public interface IExamRepository : IBaseRepository<Exam>
    {
        void Update(Exam updatedExam);
    }
}
