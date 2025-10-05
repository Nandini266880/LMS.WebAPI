using LMS.Application.Services;
using LMS.Application.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LMS.WebAPI.Controllers
{
    [ApiController]
    [Route("api/progress")]
    public class ProgressController : ControllerBase
    {
        private readonly IProgressService _progressService;

        public ProgressController(IProgressService progressService)
        {
            _progressService = progressService;
        }

        [HttpGet("{userId}/{lessonId}")]
        public async Task<IActionResult> Get(int userId, int lessonId)
        {
            var progress = await _progressService.GetProgressAsync(userId, lessonId);
            if (progress == null) return NotFound();
            return Ok(progress);
        }

        [HttpPost("{userId}/{lessonId}/complete")]
        public async Task<IActionResult> Complete(int userId, int lessonId)
        {
            await _progressService.MarkCompletedAsync(userId, lessonId);
            return Ok();
        }
    }
}
