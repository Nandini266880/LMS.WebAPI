using LMS.Application.IRepository;
using LMS.Application.Services;
using LMS.Domain.Entities;
using LMS.Domain.Enums;
using Moq;

namespace LMS.WebAPI.Tests
{
    public class PaymentServiceTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IPaymentRepository> _paymentRepoMock;
        private readonly PaymentService _paymentService;
        public PaymentServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _paymentRepoMock = new Mock<IPaymentRepository>();

            _uowMock.Setup(u => u.Payments).Returns(_paymentRepoMock.Object);
            _paymentService = new PaymentService(_uowMock.Object);
        }

        //[Fact]
        ////FunctionToTest_Result_Condition
        //public async Task CreatePayment_ShouldThrow_WhenAmountIsZero()
        //{
        //    // Arrange
        //    var userId = 1;
        //    var courseId = 101;
        //    var amount = 0;

        //    // Act & Assert
        //    await Assert.ThrowsAsync<ArgumentException>(() =>
        //        _paymentService.CreatePaymentAsync(userId, courseId, amount));
        //}

        [Fact]
        public async Task GetPaymentsByUserId_ShouldReturnPayments_WhenUserExists()
        {
            // Arrange
            var userId = 5;
            var payments = new List<Payment>
            {
                new Payment { PaymentId = 1, UserId = userId, Amount = 100, Status = PaymentStatus.Success },
                new Payment { PaymentId = 2, UserId = userId, Amount = 200, Status = PaymentStatus.Pending }
            };

            _paymentRepoMock.Setup(r => r.GetPaymentsByUserId(userId))
                            .ReturnsAsync(payments);

            // Act
            var result = await _paymentService.GetPaymentsByUserIdAsync(userId);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.All(result, p => Assert.Equal(userId, p.UserId));
        }

        [Fact]
        public async Task GetPaymentById_ShouldReturnPayment_WhenPaymentIdMatches()
        {
            int paymentId = 1;

            var payment = new Payment { PaymentId = 2, UserId = 4, Amount = 100, Status = PaymentStatus.Pending };

            _paymentRepoMock.Setup(r => r.GetAsync(p=>p.PaymentId==paymentId)).ReturnsAsync(payment);

            var result = await _paymentService.GetPaymentByIdAsync(paymentId);

            Assert.Equal(2, result.PaymentId);
        }
    }
}
