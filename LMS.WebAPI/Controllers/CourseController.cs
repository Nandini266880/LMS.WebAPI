using LMS.Application.DTOs.Course;
using LMS.Application.IRepository;
using LMS.Domain.Entities;
using LMS.WebAPI.Exceptions;
using LMS.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.WebAPI.Controllers
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]")]
    //[Authorize]
    public class CoursesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoursesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAllCourses([FromQuery] CourseFilterParams request)
        {
            var query = _unitOfWork.Courses.GetAllQueryable();

            //Filtering
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(c => c.Title.Contains(request.Search));
            }
            if (request.MinPrice.HasValue)
            {
                query = query.Where(c => c.Price >= request.MinPrice.Value);
            }
            if (request.MaxPrice.HasValue)
            {
                query = query.Where(c => c.Price <= request.MaxPrice.Value);
            }

            var totalCount = await query.CountAsync();

            //Sorting
            query = request.SortBy?.ToLower() switch
            {
                "title" => request.SortOrder == "asc" ? query.OrderBy(c => c.Title) : query.OrderByDescending(c => c.Title),
                "price" => request.SortOrder == "asc" ? query.OrderBy(c => c.Price) : query.OrderByDescending(c => c.Price),
                "createdat" => request.SortOrder == "asc" ? query.OrderBy(c => c.CreatedAt) : query.OrderByDescending(c => c.CreatedAt),
                _ => query.OrderByDescending(c => c.CreatedAt),
            };

            //Pagination
            var pagedCourses = await query
            .Skip((request.PageIndex - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(c => new CourseDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                ThumbnailUrl = c.ThumbnailUrl,
                Price = c.Price,
                CreatedAt = c.CreatedAt,
            })
            .ToListAsync();

            return Ok(new
            {
                TotalCourses = totalCount,
                data = pagedCourses
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetCourseById(int id)
        {
            var extractedCourse = await _unitOfWork.Courses.GetAsync(u => u.Id == id);

            if (extractedCourse == null)
                throw new NotFoundException("Course Not available.");

            var course = new CourseDto
            {
                Id = extractedCourse.Id,
                Title = extractedCourse.Title,
                Description = extractedCourse.Description,
                ThumbnailUrl = extractedCourse.ThumbnailUrl,
                Price = extractedCourse.Price,
                CreatedAt = extractedCourse.CreatedAt,
            };

            return Ok(course);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Instructor")]
        public async Task<ActionResult> AddCourse(CourseDto request)
        {
            int userId = User.GetUserId();
            var course = new Course
            {
                Title = request.Title,
                Description = request.Description,
                ThumbnailUrl = request.ThumbnailUrl,
                CreatedBy = userId,
                Price = request.Price,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Courses.AddAsync(course);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Instructor")]
        public async Task<IActionResult> UpdateCourse(int id, CourseDto updatedCourse)
        {
            int userId = User.GetUserId();

            var course = await _unitOfWork.Courses.GetAsync(u => u.Id == id);
            if (course == null)
                throw new NotFoundException("Course Not Found.");

            if (course.CreatedBy != userId)
            {
                throw new UnauthorizedAccessException("You are not allowed to update this course.");
            }

            course.Title = updatedCourse.Title;
            course.Description = updatedCourse.Description;
            course.Price = updatedCourse.Price;
            course.ThumbnailUrl = updatedCourse.ThumbnailUrl;
            course.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Courses.Update(course);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Instructor")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _unitOfWork.Courses.GetAsync(u => u.Id == id);
            if (course == null)
                throw new NotFoundException("Course Not found.");

            _unitOfWork.Courses.Remove(course);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

    }

    [ApiController]
    [ApiVersion("2.0")]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class CourseV2Controller : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CourseV2Controller(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CourseDto>> GetCourseByIdAsync(int id)
        {
            var course = await _unitOfWork.Courses.GetAllQueryable()
                .Include(c => c.Lessons)
                .Select(c => new CourseDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    ThumbnailUrl = c.ThumbnailUrl,
                    Price = c.Price,
                    Lessons = c.Lessons.Select(l => new LessonDto 
                    {
                        Title = l.Title,
                        Duration = l.Duration,
                        LessonIdx = l.LessonIdx
                    }).ToList()
                })
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                throw new NotFoundException("Course not found");
            }

            return Ok(course);

        }
    }
}
