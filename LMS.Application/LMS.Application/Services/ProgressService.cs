using LMS.Application.IRepository;
using LMS.Application.Services.IServices;
using LMS.Domain.Entities;

namespace LMS.Application.Services
{
    public class ProgressService : IProgressService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRedisCacheService _cache;
        private const string CacheKeyPrefix = "progress:";

        public ProgressService(IUnitOfWork unitOfWork, IRedisCacheService cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<Progress?> GetProgressAsync(int userId, int lessonId)
        {
            string key = $"{CacheKeyPrefix}{userId}:{lessonId}";

            // 1. Try to get from Redis
            var cached = await _cache.GetAsync<Progress>(key);
            if (cached != null) return cached;

            // 2. If not found, get from DB
            var progress = await _unitOfWork.Progresses
                .GetAsync(p => p.UserId == userId && p.LessonId == lessonId);

            if (progress != null)
            {
                // 3. Save to Redis for future
                await _cache.SetAsync(key, progress, TimeSpan.FromMinutes(30));
            }

            return progress;
        }

        public async Task MarkCompletedAsync(int userId, int lessonId)
        {
            var progress = await _unitOfWork.Progresses
                .GetAsync(p => p.UserId == userId && p.LessonId == lessonId);

            if (progress == null)
            {
                progress = new Progress
                {
                    UserId = userId,
                    LessonId = lessonId,
                    IsCompleted = true,
                    CompletedAt = DateTime.UtcNow
                };
                await _unitOfWork.Progresses.AddAsync(progress);
            }
            else
            {
                progress.IsCompleted = true;
                progress.CompletedAt = DateTime.UtcNow;
                _unitOfWork.Progresses.Update(progress);
            }

            await _unitOfWork.SaveAsync();

            // Update cache
            string key = $"{CacheKeyPrefix}{userId}:{lessonId}";
            await _cache.SetAsync(key, progress, TimeSpan.FromMinutes(30));
        }
    }
}
