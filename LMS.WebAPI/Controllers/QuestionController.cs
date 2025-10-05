using LMS.Application.DTOs;
using LMS.Application.Services;
using LMS.Application.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.WebAPI.Controllers
{
    [ApiController]
    [Route("api/exam/[controller]s")]
    public class QuestionController : ControllerBase
    {
        #region Private Members
        private readonly IQuestionService _questionService;
        private readonly ILogger<QuestionController> _logger;
        #endregion

        #region Constructor
        public QuestionController(IQuestionService questionService, ILogger<QuestionController> logger)
        {
            _questionService = questionService;
            _logger = logger;
        }
        #endregion

        #region APIs

        [HttpGet("{id:int}")]
        public async Task<ActionResult<QuestionDto>> GetQuestionById(int id)
        {
            var question = await _questionService.GetQuestionByIdAsync(id);
            if (question == null)
                return NotFound("Question not found");

            return Ok(question);
        }

        [HttpGet("{examId}")]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetAllQuestions(int examId)
        {
            var questions = await _questionService.GetAllQuestionsAsync(examId);
            return Ok(questions);
        }

        [Authorize(Roles = "Instructor")]
        [HttpPost("{examId}")]
        public async Task<ActionResult<QuestionDto>> CreateQuestion(int examId, [FromBody] QuestionCreateDto request)
        {
            try
            {
                var question = await _questionService.CreateQuestionAsync(examId, request);
                return CreatedAtAction(nameof(GetQuestionById), new { id = question.Id }, question);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating question");
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Instructor")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var deleted = await _questionService.DeleteQuestionAsync(id);
            if (!deleted)
                return NotFound("Question not found");

            return Ok(new { Message = $"Question {id} has been deleted" });
        }

        #endregion
    }
}
