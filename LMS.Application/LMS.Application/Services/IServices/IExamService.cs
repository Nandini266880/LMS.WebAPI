using LMS.Application.DTOs;
using LMS.Domain.Entities;

namespace LMS.Application.Services.IServices
{
    public interface IExamService
    {
        Task<ExamDto> CreateExamAsync(CreateExamDto request);
        Task<IEnumerable<ExamDto>> GetAllExamsOfCourse(int courseId);
    }
}
