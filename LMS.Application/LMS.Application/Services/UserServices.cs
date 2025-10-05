using LMS.Application.DTOs;
using LMS.Application.IRepository;
using LMS.Application.IServices;
using LMS.Domain.Entities;
using LMS.Domain.Enums;

namespace LMS.Application.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            var existingUser = await _unitOfWork.Users.GetAsync(u => u.Email == request.Email);
            if (existingUser != null)
                throw new ApplicationException("Email is already registered!");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = hashedPassword,
                Role = Enum.Parse<UserRole>(request.Role),
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveAsync();

            return new RegisterResponse
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _unitOfWork.Users.GetAllAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _unitOfWork.Users.GetAsync(u => u.Id == id);
        }

        public async Task<bool> UpdateUserAsync(int id, UpdateUserRequest request)
        {
            var existingEmailUser = await _unitOfWork.Users.GetAsync(u => u.Email == request.Email && u.Id != id);
            if (existingEmailUser != null)
                throw new ApplicationException("Email is already registered by another user.");

            var user = await _unitOfWork.Users.GetAsync(u => u.Id == id);
            if (user == null)
                return false;

            user.FullName = request.FullName;
            user.Email = request.Email;
            user.Role = Enum.Parse<UserRole>(request.Role);
            user.UpdatedAt = DateTime.UtcNow;

            if (!string.IsNullOrEmpty(request.Password))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            }

            _unitOfWork.Users.Update(user); 
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _unitOfWork.Users.GetAsync(u => u.Id == id);
            if (user == null) return false;

            _unitOfWork.Users.Remove(user);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
