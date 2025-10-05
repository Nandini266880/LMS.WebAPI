using LMS.Application.DTOs;
using LMS.Application.IRepository;
using LMS.Application.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.WebAPI.Controllers
{
    [Authorize]
    [Route("/api/courses/{courseId}/[controller]s")]
    [ApiController]
    public class ExamController : Controller
    {
        private readonly IExamService _examService;

        public ExamController(IExamService examService)
        {
            _examService = examService;
        }

        [HttpPost]
        [Authorize(Roles = "Instructor")]
        public async Task<ActionResult<ExamDto>> AddExamToCourse(int courseId, [FromBody] CreateExamDto request)
        {
            if (courseId != request.CourseId)
            {
                return BadRequest("CourseId in URL and request body must match.");
            }

            try
            {
                var exam = await _examService.CreateExamAsync(request);
                return CreatedAtAction(nameof(GetExamsOfCourse), new { courseId = exam.CourseId }, exam);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamDto>>> GetExamsOfCourse(int courseId)
        {
            try
            {
                var exams = await _examService.GetAllExamsOfCourse(courseId);
                return Ok(exams);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
