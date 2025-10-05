using LMS.Domain.Entities;
using LMS.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        #region DB SETS
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<ExamAttempt> ExamAttempts { get; set; }
        public DbSet<Progress> Progresses { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            //User -> Courses
            modelBuilder.Entity<User>()
                .HasMany(u => u.Courses)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Course -> Lessons
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Lessons)
                .WithOne(l => l.Course)
                .HasForeignKey(l => l.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Course -> Exams
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Exams)
                .WithOne(e => e.Course)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Exam -> Questions
            modelBuilder.Entity<Exam>()
                .HasMany(e => e.Questions)
                .WithOne(q => q.Exam)
                .HasForeignKey(q => q.ExamId)
                .OnDelete(DeleteBehavior.Cascade);

            // Question -> Options
            modelBuilder.Entity<Question>()
                .HasMany(q => q.Options)
                .WithOne(o => o.Question)
                .HasForeignKey(o => o.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Enrollment -> User
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.User)
                .WithMany(u => u.Enrollments)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Enrollment -> Course
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Lesson>()
                .HasIndex(u => new { u.CourseId, u.LessonIdx })
                .IsUnique();

            modelBuilder.Entity<Payment>()
               .HasOne(e => e.User)
               .WithMany()
               .HasForeignKey(e => e.UserId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payment>()
                .HasOne(e => e.Course)
                .WithMany()
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            //ExamAttempt -> User
            modelBuilder.Entity<ExamAttempt>()
                .HasOne(e => e.User)
                .WithMany(e => e.ExamAttempts)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //ExamAttempt -> Exam
            modelBuilder.Entity<ExamAttempt>()
                .HasOne(e => e.Exam)
                .WithMany(e => e.ExamAttempts)
                .HasForeignKey(e => e.ExamId)
                .OnDelete(DeleteBehavior.Cascade);

            //Progress -> User
            modelBuilder.Entity<Progress>()
                .HasOne(e => e.User)
                .WithMany(e => e.Progresses)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //Progress -> Lesson
            modelBuilder.Entity<Progress>()
                .HasOne(e => e.Lesson)
                .WithMany(e => e.Progresses)
                .HasForeignKey(e => e.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Title = "Introduction to C#", Description = "Learn the basics of C# programming language and object-oriented concepts.", ThumbnailUrl = "https://example.com/thumb1.jpg", Price = 49.99, CreatedBy = 1, CreatedAt = new DateTime(2025, 1, 1), UpdatedAt = null },
                new Course { Id = 2, Title = "Advanced JavaScript", Description = "Deep dive into JavaScript, including ES6+, asynchronous programming, and modern frameworks.", ThumbnailUrl = "https://example.com/thumb2.jpg", Price = 59.99, CreatedBy = 2, CreatedAt = new DateTime(2025, 1, 2), UpdatedAt = null },
                new Course { Id = 3, Title = "Python for Data Science", Description = "Use Python to analyze data, visualize insights, and apply machine learning models.", ThumbnailUrl = "https://example.com/thumb3.jpg", Price = 69.99, CreatedBy = 3, CreatedAt = new DateTime(2025, 1, 3), UpdatedAt = null },
                new Course { Id = 4, Title = "Web Development with React", Description = "Build dynamic web applications using React, Redux, and modern frontend tooling.", ThumbnailUrl = "https://example.com/thumb4.jpg", Price = 79.99, CreatedBy = 1, CreatedAt = new DateTime(2025, 1, 4), UpdatedAt = null },
                new Course { Id = 5, Title = "SQL for Beginners", Description = "Learn how to query, manipulate, and manage data using SQL databases.", ThumbnailUrl = "https://example.com/thumb5.jpg", Price = 39.99, CreatedBy = 2, CreatedAt = new DateTime(2025, 1, 5), UpdatedAt = null },
                new Course { Id = 6, Title = "ASP.NET Core MVC", Description = "Create robust web applications using ASP.NET Core MVC framework.", ThumbnailUrl = "https://example.com/thumb6.jpg", Price = 59.99, CreatedBy = 1, CreatedAt = new DateTime(2025, 1, 6), UpdatedAt = null },
                new Course { Id = 7, Title = "Machine Learning Basics", Description = "Understand fundamental concepts of machine learning and build predictive models.", ThumbnailUrl = "https://example.com/thumb7.jpg", Price = 89.99, CreatedBy = 3, CreatedAt = new DateTime(2025, 1, 7), UpdatedAt = null },
                new Course { Id = 8, Title = "Frontend with HTML & CSS", Description = "Master HTML and CSS to design beautiful and responsive web pages.", ThumbnailUrl = "https://example.com/thumb8.jpg", Price = 29.99, CreatedBy = 2, CreatedAt = new DateTime(2025, 1, 8), UpdatedAt = null },
                new Course { Id = 9, Title = "Java Fundamentals", Description = "Learn Java programming, object-oriented principles, and application development.", ThumbnailUrl = "https://example.com/thumb9.jpg", Price = 49.99, CreatedBy = 3, CreatedAt = new DateTime(2025, 1, 9), UpdatedAt = null },
                new Course { Id = 10, Title = "Node.js & Express", Description = "Build scalable backend applications using Node.js and Express framework.", ThumbnailUrl = "https://example.com/thumb10.jpg", Price = 59.99, CreatedBy = 1, CreatedAt = new DateTime(2025, 1, 10), UpdatedAt = null },
                new Course { Id = 11, Title = "Game Development with Unity", Description = "Create 2D and 3D games using Unity engine and C# scripting.", ThumbnailUrl = "https://example.com/thumb30.jpg", Price = 79.99, CreatedBy = 3, CreatedAt = new DateTime(2025, 1, 30), UpdatedAt = null }
            );

            modelBuilder.Entity<Lesson>().HasData(
                new Lesson { Id = 1, CourseId = 1, Title = "Introduction to C# Environment", ContentUrl = "", Duration = 30, LessonIdx = 1, CreatedAt = new DateTime(2025, 1, 1) },
                new Lesson { Id = 2, CourseId = 1, Title = "Variables and Data Types", ContentUrl = "", Duration = 45, LessonIdx = 2, CreatedAt = new DateTime(2025, 1, 1) },
                new Lesson { Id = 3, CourseId = 2, Title = "JavaScript ES6 Features", ContentUrl = "", Duration = 40, LessonIdx = 1, CreatedAt = new DateTime(2025, 1, 2) },
                new Lesson { Id = 4, CourseId = 2, Title = "Asynchronous JavaScript", ContentUrl = "", Duration = 50, LessonIdx = 2, CreatedAt = new DateTime(2025, 1, 2) },
                new Lesson { Id = 5, CourseId = 3, Title = "Python Basics", ContentUrl = "", Duration = 35, LessonIdx = 1, CreatedAt = new DateTime(2025, 1, 3) },
                new Lesson { Id = 6, CourseId = 3, Title = "Data Analysis with Pandas", ContentUrl = "", Duration = 50, LessonIdx = 2, CreatedAt = new DateTime(2025, 1, 3) },
                new Lesson { Id = 7, CourseId = 4, Title = "React Components", ContentUrl = "", Duration = 40, LessonIdx = 1, CreatedAt = new DateTime(2025, 1, 4) },
                new Lesson { Id = 8, CourseId = 4, Title = "State and Props", ContentUrl = "", Duration = 45, LessonIdx = 2, CreatedAt = new DateTime(2025, 1, 4) },
                new Lesson { Id = 9, CourseId = 5, Title = "Introduction to SQL", ContentUrl = "", Duration = 30, LessonIdx = 1, CreatedAt = new DateTime(2025, 1, 5) },
                new Lesson { Id = 10, CourseId = 5, Title = "Advanced SQL Queries", ContentUrl = "", Duration = 50, LessonIdx = 2, CreatedAt = new DateTime(2025, 1, 5) }
            );

            modelBuilder.Entity<Enrollment>().HasData( 
                new Enrollment { Id = 1, UserId = 2, CourseId = 1, EnrolledAt = new DateTime(2025, 2, 1), Status = EnrollStatus.Active },
                new Enrollment { Id = 2, UserId = 2, CourseId = 5, EnrolledAt = new DateTime(2025, 2, 5), Status = EnrollStatus.Completed },
                new Enrollment { Id = 3, UserId = 3, CourseId = 2, EnrolledAt = new DateTime(2025, 2, 2), Status = EnrollStatus.Active },
                new Enrollment { Id = 4, UserId = 3, CourseId = 6, EnrolledAt = new DateTime(2025, 2, 6), Status = EnrollStatus.Cancelled },
                new Enrollment { Id = 5, UserId = 4, CourseId = 3, EnrolledAt = new DateTime(2025, 3, 3), Status = EnrollStatus.Cancelled },
                new Enrollment { Id = 6, UserId = 4, CourseId = 7, EnrolledAt = new DateTime(2025, 3, 7), Status = EnrollStatus.Completed }
            );

            modelBuilder.Entity<Exam>().HasData(
                new Exam { Id = 1, CourseId = 1, Title = "C# Basics Quiz", TotalMarks = 50, CreatedAt = new DateTime(2025, 2, 15) },
                new Exam { Id = 2, CourseId = 2, Title = "JavaScript ES6 Test", TotalMarks = 60, CreatedAt = new DateTime(2025, 2, 20) },
                new Exam { Id = 3, CourseId = 3, Title = "Python Data Science Final", TotalMarks = 100, CreatedAt = new DateTime(2025, 3, 1) },
                new Exam { Id = 4, CourseId = 4, Title = "React Components Midterm", TotalMarks = 80, CreatedAt = new DateTime(2025, 3, 5) },
                new Exam { Id = 5, CourseId = 5, Title = "SQL Fundamentals Quiz", TotalMarks = 40, CreatedAt = new DateTime(2025, 3, 10) },
                new Exam { Id = 6, CourseId = 2, Title = "JS Mid Term", TotalMarks = 90, CreatedAt = new DateTime(2025, 3, 15) }
            );

            modelBuilder.Entity<Question>().HasData(
                new Question { Id = 1, ExamId = 1, QuestionText = "What is the size of int in C#?", Marks = 5 },
                new Question { Id = 2, ExamId = 1, QuestionText = "Which keyword is used to define a class in C#?", Marks = 5 },
                new Question { Id = 3, ExamId = 2, QuestionText = "Which of the following is a JavaScript ES6 feature?", Marks = 5 },
                new Question { Id = 4, ExamId = 2, QuestionText = "Which method is used to fetch data asynchronously in JavaScript?", Marks = 5 },
                new Question { Id = 5, ExamId = 5, QuestionText = "Which SQL keyword is used to retrieve data from a database?", Marks = 5 },
                new Question { Id = 6, ExamId = 5, QuestionText = "Which SQL clause is used to filter records?", Marks = 5 },
                new Question { Id = 7, ExamId = 1, QuestionText = "Which of the following are OOP principles?", Marks = 5 }
            );

            modelBuilder.Entity<Option>().HasData(
                // Question 1
                new Option { Id = 1, QuestionId = 1, OptionText = "2 bytes", IsCorrect = false },
                new Option { Id = 2, QuestionId = 1, OptionText = "4 bytes", IsCorrect = true },
                new Option { Id = 3, QuestionId = 1, OptionText = "8 bytes", IsCorrect = false },

                // Question 2
                new Option { Id = 4, QuestionId = 2, OptionText = "define", IsCorrect = false },
                new Option { Id = 5, QuestionId = 2, OptionText = "class", IsCorrect = true },
                new Option { Id = 6, QuestionId = 2, OptionText = "struct", IsCorrect = false },

                // Question 3
                new Option { Id = 7, QuestionId = 3, OptionText = "Arrow Functions", IsCorrect = true },
                new Option { Id = 8, QuestionId = 3, OptionText = "Goto Statement", IsCorrect = false },
                new Option { Id = 9, QuestionId = 3, OptionText = "Pipelines", IsCorrect = false },

                // Question 4
                new Option { Id = 10, QuestionId = 4, OptionText = "fetch()", IsCorrect = true },
                new Option { Id = 11, QuestionId = 4, OptionText = "getData()", IsCorrect = false },
                new Option { Id = 12, QuestionId = 4, OptionText = "httpRequest()", IsCorrect = false },

                // Question 5
                new Option { Id = 13, QuestionId = 5, OptionText = "SELECT", IsCorrect = true },
                new Option { Id = 14, QuestionId = 5, OptionText = "GET", IsCorrect = false },
                new Option { Id = 15, QuestionId = 5, OptionText = "FETCH", IsCorrect = false },

                // Question 6
                new Option { Id = 16, QuestionId = 6, OptionText = "WHERE", IsCorrect = true },
                new Option { Id = 17, QuestionId = 6, OptionText = "ORDER BY", IsCorrect = false },
                new Option { Id = 18, QuestionId = 6, OptionText = "GROUP BY", IsCorrect = false },

                //Question 7
                new Option { Id = 19, QuestionId = 7, OptionText = "Encapsulation", IsCorrect = true },
                new Option { Id = 20, QuestionId = 7, OptionText = "Inheritance", IsCorrect = true },
                new Option { Id = 21, QuestionId = 7, OptionText = "Polymorphism", IsCorrect = true },
                new Option { Id = 22, QuestionId = 7, OptionText = "Recursion", IsCorrect = false },
                new Option { Id = 23, QuestionId = 7, OptionText = "Abstraction", IsCorrect = true }

            );


            modelBuilder.Entity<ExamAttempt>().HasData(
                new ExamAttempt { AttemptId = 1, ExamId = 1, UserId = 1, Score = 85, AttemptedAt = new DateTime(2025, 01, 05, 10, 0, 0, DateTimeKind.Utc) },
                new ExamAttempt { AttemptId = 2, ExamId = 1, UserId = 2, Score = 72, AttemptedAt = new DateTime(2025, 01, 06, 11, 30, 0, DateTimeKind.Utc) },
                new ExamAttempt { AttemptId = 3, ExamId = 2, UserId = 1, Score = 90, AttemptedAt = new DateTime(2025, 01, 08, 14, 0, 0, DateTimeKind.Utc) },
                new ExamAttempt { AttemptId = 4, ExamId = 2, UserId = 3, Score = 60, AttemptedAt = new DateTime(2025, 01, 09, 16, 0, 0, DateTimeKind.Utc) },
                new ExamAttempt { AttemptId = 5, ExamId = 3, UserId = 2, Score = 77, AttemptedAt = new DateTime(2025, 01, 10, 18, 0, 0, DateTimeKind.Utc) }
            );


            modelBuilder.Entity<Progress>().HasData(
                new Progress { ProgressId = 1, UserId = 1, LessonId = 1, IsCompleted = true, CompletedAt = new DateTime(2025, 01, 10, 12, 0, 0, DateTimeKind.Utc) },
                new Progress { ProgressId = 2, UserId = 1, LessonId = 2, IsCompleted = false, CompletedAt = null },
                new Progress { ProgressId = 3, UserId = 2, LessonId = 3, IsCompleted = true, CompletedAt = new DateTime(2025, 01, 12, 15, 30, 0, DateTimeKind.Utc) },
                new Progress { ProgressId = 4, UserId = 3, LessonId = 4, IsCompleted = true, CompletedAt = new DateTime(2025, 01, 15, 09, 0, 0, DateTimeKind.Utc) },
                new Progress { ProgressId = 5, UserId = 2, LessonId = 5, IsCompleted = false, CompletedAt = null }
            );
        }
    }
}
