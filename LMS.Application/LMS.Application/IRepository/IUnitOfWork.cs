using System;

namespace LMS.Application.IRepository
{
    public interface IUnitOfWork
    {
        //Custom Repo Object
        public IUserRepository Users { get; }
        public ICourseRepository Courses { get; }
        public ILessonRepository Lessons { get; }
        public IEnrollmentRepository Enrollments { get; }
        public IPaymentRepository Payments { get; }
        public IExamRepository Exams { get; }
        public IQuestionRepository Questions { get; }
        public IExamAttemptRepository ExamAttempts { get; }

        public IProgressRepository Progresses { get; }
        public Task SaveAsync();
    }
}
