using LMS.Application.DTOs;
using LMS.Application.IRepository;
using LMS.Application.Services.IServices;
using LMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Services
{
    public class ExamAttemptService : IExamAttemptService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ExamAttemptService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ExamAttemptDto> AddExamAttemptAsync(int userId, int examId, List<UserAnswerDto> userAnswers)
        {
            int totalScore = 0;

            var questions = await _unitOfWork.Questions
                .GetAllQueryable()
                .Where(q => q.ExamId == examId)
                .Include(op => op.Options)
                .ToListAsync();

            foreach (var question in questions)
            {
                var userAnswer = userAnswers.FirstOrDefault(u => u.QuestionId == question.Id);
                if (userAnswer == null) continue; // User skipped this question

                // Get correct options
                var correctOptionIds = question.Options
                        .Where(o => o.IsCorrect)
                        .Select(o => o.Id)
                        .ToList();

                // Compare with user selected options
                if (correctOptionIds.Count == userAnswer.SelectedOptionIds.Count &&
                    !correctOptionIds.Except(userAnswer.SelectedOptionIds).Any())
                {
                    // All correct options selected, no extra wrong options
                    totalScore += question.Marks;
                }
            }

            var attempt = new ExamAttempt
            {
                ExamId = examId,
                UserId = userId,
                Score = totalScore,
                AttemptedAt = DateTime.UtcNow
            };

            await _unitOfWork.ExamAttempts.AddAsync(attempt);
            await _unitOfWork.SaveAsync();

            return MapToDto(attempt);
        }

        public async Task<ExamAttemptDto> GetExamAttemptById(int attemptId)
        {
            var exam = await _unitOfWork.ExamAttempts.GetAsync(u => u.AttemptId == attemptId);
            if (exam == null)
            {
                throw new KeyNotFoundException($"Exam Attempt {attemptId} is invalid.");
            }
            return MapToDto(exam);
        }

        public async Task<IEnumerable<ExamAttemptDto>> GetExamAttemptsOfUser(int userId)
        {
            bool userExists = await _unitOfWork.ExamAttempts.UserExistsAsync(userId);
            if (!userExists)
            {
                throw new KeyNotFoundException($"Exams with ID {userId} not found.");
            }

            var exams = await _unitOfWork.ExamAttempts.GetAttemptsByUserAsync(userId);

            return exams.Select(MapToDto);
        }

        #region Helper
        public ExamAttemptDto MapToDto(ExamAttempt examAttemptDto)
        {
            return new ExamAttemptDto
            {
                UserId = examAttemptDto.UserId,
                ExamId = examAttemptDto.ExamId,
                Score = examAttemptDto.Score,
                AttemptedAt = examAttemptDto.AttemptedAt,
            };
        }
        #endregion
    }
}
