using LMS.Application.DTOs;
using LMS.Application.Services.IServices;
using LMS.WebAPI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LMS.WebAPI.Controllers
{
    [ApiController]
    [Route("/api/exam-attempts")]
    public class ExamAttemptController : Controller
    {
        private readonly IExamAttemptService _examAttemptServices;
        public ExamAttemptController(IExamAttemptService examAttemptService)
        {
            _examAttemptServices = examAttemptService;
        }

        [HttpGet("{attemptId}")]
        public async Task<ActionResult<ExamAttemptDto>> GetAttemptedExam(int attemptId)
        {
            try
            {
                var attemptedExam =  await _examAttemptServices.GetExamAttemptById(attemptId);
                return Ok(attemptedExam);

            }
            catch(KeyNotFoundException ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("user")]
        public async Task<ActionResult<IEnumerable<ExamAttemptDto>>> GetAllExamsOfUser()
        {
            int userId = HttpContext.User.GetUserId();
            try
            {
                var questions = await _examAttemptServices.GetExamAttemptsOfUser(userId);
                return Ok(questions);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPost("{examId}/submit")]
        public async Task<IActionResult> SubmitExam(int examId, List<UserAnswerDto> userAnswers)
        {
            //int userId = HttpContext.User.GetUserId();
            var attempt = await _examAttemptServices.AddExamAttemptAsync(3, examId, userAnswers);
            if(attempt==null)
            {
                return BadRequest();
            }
            return Ok(userAnswers);
        }

    }
}
