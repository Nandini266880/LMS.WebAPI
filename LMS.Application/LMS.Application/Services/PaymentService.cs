using LMS.Application.IRepository;
using LMS.Application.Services.IServices;
using LMS.Application.DTOs;
using LMS.Domain.Entities;
using LMS.Domain.Enums;

namespace LMS.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Methods
        public async Task<PaymentResponseDto> CreatePaymentAsync(int userId, PaymentRequestDto request)
        {
            if (request.Amount <= 0)
                throw new ArgumentException("Amount must be greater than zero");

            var course = await _unitOfWork.Courses.GetAsync(c => c.Id == request.CourseId);
            if (course == null)
                throw new KeyNotFoundException("Course not found");

            if (request.Amount != Math.Round(course.Price, 2))
                throw new InvalidOperationException("Payment amount must exactly match the course price.");

            var payment = new Payment
            {
                UserId = userId,
                CourseId = request.CourseId,
                Amount = request.Amount,
                Status = PaymentStatus.Pending,
                PaymentDate = DateTime.UtcNow
            };

            await _unitOfWork.Payments.AddAsync(payment);
            await _unitOfWork.SaveAsync();

            return MapToDto(payment);
        }

        public async Task<bool> UpdatePaymentStatusAsync(int paymentId, PaymentStatus status)
        {
            var payment = await _unitOfWork.Payments.GetAsync(p => p.PaymentId == paymentId);

            if (payment == null)
                throw new KeyNotFoundException("Payment not found");

            if (payment.Status != PaymentStatus.Pending)
                return false;

            payment.Status = status;
            _unitOfWork.Payments.UpdateAsync(payment);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<PaymentResponseDto?> GetPaymentByIdAsync(int paymentId)
        {
            var payment = await _unitOfWork.Payments.GetAsync(p => p.PaymentId == paymentId);
            return payment == null ? null : MapToDto(payment);
        }

        public async Task<IEnumerable<PaymentResponseDto>> GetPaymentsByUserIdAsync(int userId)
        {
            var payments = await _unitOfWork.Payments.GetPaymentsByUserId(userId);
            return payments.Select(MapToDto);
        }

        public async Task<IEnumerable<PaymentResponseDto>> GetAllPaymentsAsync()
        {
            var payments = await _unitOfWork.Payments.GetAllAsync();
            return payments.Select(MapToDto);
        }

        public async Task<bool> DeletePaymentAsync(int id)
        {
            var payment = await _unitOfWork.Payments.GetAsync(p => p.PaymentId == id);
            if (payment == null) return false;

            _unitOfWork.Payments.Remove(payment);
            await _unitOfWork.SaveAsync();
            return true;
        }

        #endregion

        #region Helpers
        private static PaymentResponseDto MapToDto(Payment payment)
        {
            return new PaymentResponseDto
            {
                PaymentId = payment.PaymentId,
                UserId = payment.UserId,
                CourseId = payment.CourseId,
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                Status = payment.Status
            };
        }
        #endregion
    }
}
