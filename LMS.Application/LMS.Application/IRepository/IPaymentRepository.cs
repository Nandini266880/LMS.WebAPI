using LMS.Domain.Entities;
using LMS.Domain.Enums;

namespace LMS.Application.IRepository
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        Task<IEnumerable<Payment>> GetPaymentsByUserId(int userId);
        void UpdateAsync(Payment payment);
        
    }
}
