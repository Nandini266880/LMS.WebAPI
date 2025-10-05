using LMS.Application.IRepository;
using LMS.Domain.Entities;
using LMS.WebAPI.Exceptions;
using LMS.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LMS.WebAPI.Controllers
{
    [Route("/api/[controller]s")]
    [ApiController]
    public class LessonController : Controller
    {
        #region Private Members
        private readonly IUnitOfWork _unitOfWork;

        //References: https://swagger.io/docs/specification/v3_0/describing-request-body/file-upload/
        private string GetPathOfFile(LessonCreateDto request)
        {
            string contentUrl = "";

            if (request.File != null && request.File.Length > 0)
            {
                var uploadPath = Path.Combine("wwwroot", "lesson-files");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var fileName = $"{Guid.NewGuid()}_{request.File.FileName}";
                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    request.File.CopyToAsync(stream);
                }

                contentUrl = $"/lesson-files/{fileName}";
            }
            return contentUrl;
        }
        #endregion

        #region Controller
        public LessonController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region WEB APIs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lesson>>> GetAllLessons(int pageIndex = 1, int pageSize = 10)
        {
            var query = _unitOfWork.Lessons.GetAllQueryable();
            var totalCount = await query.CountAsync();

            var pagedLessons = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

            return Ok(new
            {
                TotalLessons = totalCount,
                data = pagedLessons
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Instructor")]
        public async Task<ActionResult> AddLesson(int courseId,[FromForm] LessonCreateDto request)
        {
            int userId = User.GetUserId();

            var course = await _unitOfWork.Courses.GetAsync(u => u.Id == courseId);
            if (course == null)
            {
                return NotFound("Course doesn't exist.");
            }

            var userRole = User.FindFirst(ClaimTypes.Role);
            if (userRole.Value != "Admin" && course.CreatedBy != userId)
            {
                throw new UnauthorizedAccessException("You don't have access to this course.");
            }

            var lessonCount = await _unitOfWork.Lessons.GetAllQueryable()
                .Where(u => u.CourseId == courseId)
                .Select(l => (int?)l.LessonIdx)
                .MaxAsync() ?? 0;

            var lesson = new Lesson
            {
                CourseId = courseId,
                Title = request.Title,
                ContentUrl = GetPathOfFile(request),
                Duration = request.Duration,
                LessonIdx = lessonCount + 1,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Lessons.AddAsync(lesson);
            await _unitOfWork.SaveAsync();
            return Ok(new
            {
                lesson.Title, lesson.ContentUrl, lesson.Duration
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Lesson>> GetLessonById(int id)
        {
            var lesson = await _unitOfWork.Lessons.GetAsync(u => u.Id == id);

            if (lesson == null)
                throw new NotFoundException("Lesson Not available.");

            return Ok(new 
            {
                lesson.Title,
                lesson.ContentUrl,
                lesson.Duration,
                lesson.LessonIdx,
                lesson.CreatedAt
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Instructor")]
        public async Task<IActionResult> UpdateLesson(int id,[FromForm] LessonCreateDto updatedLesson)
        {
            int userId = User.GetUserId();
            var lesson = await _unitOfWork.Lessons.GetAsync(u => u.Id == id);

            var userRole = User.FindFirst(ClaimTypes.Role);
            if (userRole.Value != "Admin" && lesson.Course.CreatedBy != userId)
            {
                throw new UnauthorizedAccessException("You don't have access to this lesson.");
            }

            if (lesson == null)
                throw new NotFoundException("Lesson Not Found.");

            lesson.Title = updatedLesson.Title;
            lesson.ContentUrl = GetPathOfFile(updatedLesson);
            lesson.Duration = updatedLesson.Duration;

            _unitOfWork.Lessons.Update(lesson);
            await _unitOfWork.SaveAsync();
            return Ok(new
            {
                lesson.Title,
                lesson.ContentUrl,
                lesson.Duration
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Instructor")]
        public async Task<IActionResult> DeleteLesson(int id)
        {
            var lesson = await _unitOfWork.Lessons.GetAsync(u => u.Id == id);
            if (lesson == null)
                throw new NotFoundException("Lesson Not found.");

            _unitOfWork.Lessons.Remove(lesson);
            await _unitOfWork.SaveAsync();
            return Ok("Lesson has been deleted.");
        }
        #endregion
    }
}
