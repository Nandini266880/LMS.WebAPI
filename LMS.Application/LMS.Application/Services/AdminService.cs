using LMS.Application.DTOs;
using LMS.Application.IRepository;
using LMS.Application.Services.IServices;

namespace LMS.Application.Services
{
    public class AdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRedisCacheService _cache;

        public AdminService(IUnitOfWork unitOfWork, IRedisCacheService cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<StudentCourseProgressDto> GetStudentCourseProgress(int studentId, int courseId)
        {
            string cacheKey = $"studentProgress:{studentId}:{courseId}";

            // Try Redis first
            var cached = await _cache.GetAsync<StudentCourseProgressDto>(cacheKey);
            if (cached != null) return cached;

            // Query DB
            var lessons = _unitOfWork.Lessons
                .GetAllQueryable()
                .Where(l => l.CourseId == courseId)
                .OrderBy(l => l.LessonIdx)
                .ToList();

            var progress = _unitOfWork.Progresses
                .GetAllQueryable()
                .Where(p => p.UserId == studentId && p.Lesson.CourseId == courseId)
                .ToList();


            int totalLessons = lessons.Count;
            int completedLessons = progress.Count(p => p.IsCompleted);

            var result = new StudentCourseProgressDto
            {
                StudentId = studentId,
                CourseId = courseId,
                TotalLessons = totalLessons,
                CompletedLessons = completedLessons,
                CompletionPercentage = totalLessons == 0 ? 0 : (completedLessons * 100) / totalLessons
            };

            // Cache for 10 minutes
            await _cache.SetAsync(cacheKey, result, TimeSpan.FromMinutes(20));

            return result;
        }
    }

}
