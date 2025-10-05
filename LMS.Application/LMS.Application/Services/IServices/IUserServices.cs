using LMS.Application.DTOs;
using LMS.Domain.Entities;

namespace LMS.Application.IServices
{
    public interface IUserServices
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<bool> UpdateUserAsync(int id, UpdateUserRequest request);
        Task<bool> DeleteUserAsync(int id);

    }
}
