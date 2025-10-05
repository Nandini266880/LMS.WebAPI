using LMS.Application.DTOs;
using LMS.Domain.Entities;
using LMS.Domain.Enums;

namespace LMS.Application.Services.IServices
{
    public interface IQuestionService
    {
        Task<QuestionDto> CreateQuestionAsync(int examId, QuestionCreateDto request);
        Task<QuestionDto?> GetQuestionByIdAsync(int id);
        Task<IEnumerable<QuestionDto>> GetAllQuestionsAsync(int examId);
        Task<bool> DeleteQuestionAsync(int id);
    }
}
