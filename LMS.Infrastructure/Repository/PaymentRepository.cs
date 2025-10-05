using LMS.Application.IRepository;
using LMS.Domain.Entities;
using LMS.Domain.Enums;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repository
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        private readonly AppDBContext _db;
        public PaymentRepository(AppDBContext db) : base(db) 
        {
            _db = db;
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByUserId(int userId)
        {
            return await _db.Payments
                    .Where(p => p.UserId == userId)
                    .OrderByDescending(p => p.PaymentDate)
                    .ToListAsync();
        }
        public void UpdateAsync(Payment payment)
        {
            _db.Payments.Update(payment);
        }
    }
}
