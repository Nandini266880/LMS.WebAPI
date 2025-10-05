using LMS.Application.DTOs;
using LMS.Application.IRepository;
using LMS.Application.Services.IServices;
using LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

//references for catching: https://medium.com/@nwonahr/caching-strategies-in-asp-net-core-web-api-giving-your-api-a-superpower-a67314fe5a2a
namespace LMS.Application.Services
{
    public class ExamService : IExamService
    {
        #region Private Members
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ExamService> _logger;
        private readonly IMemoryCache _cache;
        private const string ExamsCacheKey = "ExamList";
        #endregion

        public ExamService(IUnitOfWork unitOfWork, IMemoryCache cache, ILogger<ExamService> logger)
        {
            _unitOfWork = unitOfWork;   
            _cache = cache;
            _logger = logger;
        }
        public async Task<ExamDto> CreateExamAsync(CreateExamDto request)
        {
            var course = await _unitOfWork.Courses.GetAsync(c => c.Id == request.CourseId);
            if (course == null)
            {
                throw new KeyNotFoundException("Course doesn't exist.");
            }

            var exam = new Exam
            {
                CourseId = request.CourseId,
                Title = request.Title,
                TotalMarks = request.TotalMarks,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Exams.AddAsync(exam);
            await _unitOfWork.SaveAsync();

            var examDto = new ExamDto
            {
                Id = exam.Id,
                CourseId = exam.CourseId,
                Title = exam.Title,
                TotalMarks = exam.TotalMarks,
                CreatedAt = exam.CreatedAt
            };

            return examDto;
        }

        public async Task<IEnumerable<ExamDto>> GetAllExamsOfCourse(int courseId)
        {
            if(_cache.TryGetValue(courseId, out IEnumerable<ExamDto>? exams))
            {
                _logger.LogInformation("Exams list found in cache.");
                return exams;
            }
            else
            {
                _logger.LogInformation("Exam list not found in cache.");

                var course = await _unitOfWork.Courses.GetAsync(c => c.Id == courseId);
                if (course == null)
                {
                    throw new KeyNotFoundException("Course doesn't exist.");
                }

                exams = await _unitOfWork.Exams.GetAllQueryable()
                .Where(e => e.CourseId == courseId)
                .Select(e => new ExamDto
                {
                    Id = e.Id,
                    CourseId = e.CourseId,
                    Title = e.Title,
                    TotalMarks = e.TotalMarks,
                    CreatedAt = e.CreatedAt
                })
                .ToListAsync();

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromSeconds(60),
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
                };

                _cache.Set(ExamsCacheKey, exams, cacheEntryOptions);

                return exams;
            }
        }
    }
}
