using LMS.Application.IRepository;
using LMS.Infrastructure.Repository;
using LMS.Infrastructure.Data;

namespace LMS.Infrastructure.Repository
{

    public class UnitOfWork : IUnitOfWork
    {
        #region Private Members
        private readonly AppDBContext _db;
        public IUserRepository Users { get; private set; }
        public ICourseRepository Courses { get; private set; }
        public ILessonRepository Lessons { get; private set; }
        public IEnrollmentRepository Enrollments { get; private set; }
        public IPaymentRepository Payments { get; private set; }
        public IExamRepository Exams { get; private set; }
        public IQuestionRepository Questions { get; private set; }
        public IExamAttemptRepository ExamAttempts { get; private set; }
        public IProgressRepository Progresses { get; private set; }
        #endregion

        #region Constructor
        public UnitOfWork(AppDBContext db) 
        {
            _db = db;
            Users = new UserRepository(_db);
            Courses = new CourseRepository(_db);
            Lessons = new LessonRepository(_db);
            Enrollments = new EnrollmentRepository(_db);
            Payments = new PaymentRepository(_db);
            Exams = new ExamRepository(_db);
            Questions = new QuestionRepository(_db);
            ExamAttempts = new ExamAttemptRepository(_db);
            Progresses = new ProgressRepository(_db);
            //Custom Repo object creation and passing _db
        }
        #endregion
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
