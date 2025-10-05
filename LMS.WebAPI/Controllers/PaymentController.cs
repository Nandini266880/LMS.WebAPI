using LMS.Application.DTOs;
using LMS.Application.Services.IServices;
using LMS.Domain.Enums;
using LMS.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        #region Private Members
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;
        #endregion

        #region Constructor
        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }
        #endregion

        #region APIs

        [HttpPost]
        public async Task<ActionResult<PaymentResponseDto>> CreatePayment([FromBody] PaymentRequestDto request)
        {
            try
            {
                int userId = User.GetUserId();
                var payment = await _paymentService.CreatePaymentAsync(userId, request);
                return CreatedAtAction(nameof(GetPaymentById), new { id = payment.PaymentId }, payment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating payment");
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentResponseDto>>> GetAllPayments()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            return Ok(payments);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PaymentResponseDto>> GetPaymentById(int id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null)
                return NotFound("Payment not found");

            return Ok(payment);
        }

        [HttpGet("my-payments")]
        public async Task<ActionResult<IEnumerable<PaymentResponseDto>>> GetMyPayments()
        {
            try
            {
                int userId = User.GetUserId();
                var payments = await _paymentService.GetPaymentsByUserIdAsync(userId);
                return Ok(payments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user payments");
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePaymentStatus(int id, PaymentStatus status)
        {
            try
            {
                var updated = await _paymentService.UpdatePaymentStatusAsync(id, status);
                if (!updated)
                    return BadRequest("Payment status cannot be updated (maybe not pending)");

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Payment not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating payment status");
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var deleted = await _paymentService.DeletePaymentAsync(id);
            if (!deleted)
                return NotFound("Payment not found");

            return Ok(new { Message = $"Payment {id} has been deleted" });
        }

        #endregion
    }
}
