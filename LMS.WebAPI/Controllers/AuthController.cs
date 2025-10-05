using LMS.Application.DTOs;
using LMS.Application.IRepository;
using LMS.Infrastructure.JwtServices;
using LMS.WebAPI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ApiKeyAuth]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public AuthController(IUnitOfWork unitOfWork, ITokenService tokenService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        /// <summary>
        /// Login from Here
        /// </summary>
        /// <param name="Login From Here"></param>
        /// <returns></returns>

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Email and password are required.");
            }

            var user = await _unitOfWork.Users.GetAsync(u => u.Email == request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid credentials");
            }

            int expiryMinutes = Convert.ToInt32(_configuration["JwtConfig:DurationInMinutes"]);
            string secretKey = _configuration["JwtConfig:Key"];

            var newAccessToken = _tokenService.GenerateToken(user.Id, user.Email, user.Role, secretKey, expiryMinutes);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddHours(6);
            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveAsync();

            return Ok(new LoginResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                Id = user.Id,
                Email = user.Email,
                Role = user.Role
            });
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<LoginResponse>> Refresh([FromBody] TokenRequest request)
        {
            var user = await _unitOfWork.Users.GetAsync(u => u.RefreshToken == request.RefreshToken);
            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return Unauthorized("Invalid user or refresh token.");
            }

            int expiryMinutes = Convert.ToInt32(_configuration["JwtConfig:DurationInMinutes"]);
            string secretKey = _configuration["JwtConfig:Key"];

            var newAccessToken = _tokenService.GenerateToken(user.Id, user.Email, user.Role, secretKey, expiryMinutes);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddHours(6);
            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveAsync();

            return Ok(new LoginResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                Id = user.Id,
                Email = user.Email,
                Role = user.Role
            });
        }
    }
}
