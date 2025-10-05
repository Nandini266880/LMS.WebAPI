using LMS.Application.DTOs;
using LMS.Application.IRepository;
using LMS.Application.Services.IServices;
using LMS.Domain.Entities;
using LMS.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuestionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Methods
        public async Task<QuestionDto> CreateQuestionAsync(int examId, QuestionCreateDto request)
        {
            var exam = await _unitOfWork.Exams.GetAsync(e => e.Id == examId);
            if (exam == null)
                throw new KeyNotFoundException("Exam not found");

            var question = new Question
            {
                ExamId = examId,
                QuestionText = request.QuestionText,
                Marks = request.Marks
            };

            foreach (var optionDto in request.Options)
            {
                var option = new Option
                {
                    OptionText = optionDto.OptionText,
                    IsCorrect = optionDto.IsCorrect
                };
                question.Options.Add(option);
            }

            await _unitOfWork.Questions.AddAsync(question);
            await _unitOfWork.SaveAsync();

            return MapToDto(question);
        }

        public async Task<QuestionDto?> GetQuestionByIdAsync(int id)
        {
            var question = await _unitOfWork.Questions
                .GetAllQueryable()
                .Where(q => q.Id == id)
                .Include(q => q.Options)
                .FirstOrDefaultAsync();

            return question == null ? null : MapToDto(question);
        }

        public async Task<IEnumerable<QuestionDto>> GetAllQuestionsAsync(int examId)
        {
            var questions = await _unitOfWork.Questions
                .GetAllQueryable()
                .Where(q => q.ExamId == examId)
                .Include(q => q.Options)
                .ToListAsync();

            return questions.Select(q => MapToDto(q));
        }

        public async Task<bool> DeleteQuestionAsync(int id)
        {
            var question = await _unitOfWork.Questions
                .GetAllQueryable()
                .Include(q => q.Options)
                .FirstOrDefaultAsync(q => q.Id == id);

            _unitOfWork.Questions.Remove(question);
            await _unitOfWork.SaveAsync();
            return true;
        }

        #endregion

        private QuestionDto MapToDto(Question question)
        {
            return new QuestionDto
            {
                Id = question.Id,
                QuestionText = question.QuestionText,
                Marks = question.Marks,
                Options = question.Options.Select(o => new OptionDto
                {
                    Id = o.Id,
                    OptionText = o.OptionText,
                    IsCorrect = o.IsCorrect
                }).ToList()
            };
        }
    }
}
