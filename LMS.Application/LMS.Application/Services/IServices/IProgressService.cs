using LMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Services.IServices
{
    public interface IProgressService
    {
        Task<Progress?> GetProgressAsync(int userId, int lessonId);
        Task MarkCompletedAsync(int userId, int lessonId);
    }
}
