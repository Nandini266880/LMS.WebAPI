using LMS.Application.IRepository;
using LMS.Domain.Entities;
using LMS.WebAPI.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LMS.WebAPI.Controllers
{
    [Route("/api/course/{courseId}/[controller]")]
    [ApiController]
    public class LessonWithCoursesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public LessonWithCoursesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lesson>>> GetAllLessons(int courseId, int pageIndex = 1, int pageSize = 10)
        {
            var lessons = _unitOfWork.Lessons.GetAllQueryable()
                .Where(l => l.CourseId==courseId)
                .OrderBy(l => l.LessonIdx);

            if (!lessons.Any())
                throw new NotFoundException($"No lessons found for courseId = {courseId}");

            var totalCount = await lessons.CountAsync();

            var pagedLessons = await lessons
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

            return Ok(new
            {
                TotalLessons = totalCount,
                data = pagedLessons
            });
        }

        [HttpGet("{lessonIdx}")]
        public async Task<ActionResult<Lesson>> GetLessonById(int courseId, int lessonIdx)
        {
            var lesson = await _unitOfWork.Lessons.GetAsync(l => l.LessonIdx == lessonIdx && l.CourseId==courseId);
            if (lesson == null)
                throw new NotFoundException("Lesson Not available.");

            return Ok(lesson);
        }

        [HttpDelete("{lessonIdx}")]
        [Authorize(Roles = "Admin, Instructor")]
        public async Task<IActionResult> DeleteLesson(int courseId, int lessonIdx)
        {
            var lesson = await _unitOfWork.Lessons.GetAsync(u => u.CourseId == courseId && u.LessonIdx==lessonIdx);
            if (lesson == null)
                throw new NotFoundException("Lesson Not found.");

            _unitOfWork.Lessons.Remove(lesson);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

    }
}
