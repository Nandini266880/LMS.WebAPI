using LMS.Application.DTOs;
using LMS.Domain.Entities;
using LMS.Domain.Enums;

namespace LMS.Application.Services.IServices
{
    public interface IPaymentService
    {
        Task<PaymentResponseDto> CreatePaymentAsync(int userId, PaymentRequestDto request);
        Task<bool> UpdatePaymentStatusAsync(int paymentId, PaymentStatus status);
        Task<PaymentResponseDto?> GetPaymentByIdAsync(int paymentId);
        Task<IEnumerable<PaymentResponseDto>> GetPaymentsByUserIdAsync(int userId);
        Task<IEnumerable<PaymentResponseDto>> GetAllPaymentsAsync();
        Task<bool> DeletePaymentAsync(int id);
    }
}
