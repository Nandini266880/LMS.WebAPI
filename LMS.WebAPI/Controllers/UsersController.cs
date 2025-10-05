using LMS.Application.DTOs;
using LMS.Application.IServices;
using LMS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.WebAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UsersController : Controller
    {

        #region Private Members
        private readonly IUserServices _userService;
        private readonly ILogger<UsersController> _logger;
        #endregion

        #region Constructor
        public UsersController(IUserServices userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        #endregion

        #region WEB APIs

        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponse>> Register([FromBody] RegisterRequest request)
        {
            try
            {
                _logger.LogInformation("New user is being added to Database.");
                var response = await _userService.RegisterAsync(request);
                return Ok(response);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound("User doesn't exist!");
            }
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserRequest request)
        {
            try
            {
                var updated = await _userService.UpdateUserAsync(id, request);
                if (!updated)
                {
                    return NotFound("User doesn't exist!");
                }
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _userService.DeleteUserAsync(id);
            if (!deleted)
            {
                return NotFound("User doesn't exist!");
            }
            return NoContent();
        }
        #endregion
    }
}
