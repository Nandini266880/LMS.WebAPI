using LMS.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.WebAPI.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService;

        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("student/{studentId}/course/{courseId}/progress")]
        public async Task<IActionResult> GetStudentProgress(int studentId, int courseId)
        {
            var progress = await _adminService.GetStudentCourseProgress(studentId, courseId);
            return Ok(progress);
        }
    }

}
