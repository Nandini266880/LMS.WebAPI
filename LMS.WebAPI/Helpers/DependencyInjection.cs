using LMS.Application.IRepository;
using LMS.Application.IServices;
using LMS.Application.Services;
using LMS.Application.Services.IServices;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.JwtServices;
using LMS.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;

namespace LMS.WebAPI.Helpers
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddLMSDI(this IServiceCollection services, IConfiguration configuration)
        {
            //DbContext
            services.AddDbContext<AppDBContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


            #region Dependency Injections
            //App. and Infra Services
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost:6379"));
            services.AddSingleton<IRedisCacheService, RedisCacheService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<IExamService, ExamService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IExamAttemptService, ExamAttemptService>();
            services.AddScoped<IProgressService, ProgressService>();
            services.AddScoped<AdminService>();



            #endregion

            #region JWT Authentication
            //JWT Authentication
            var jwtSettings = configuration.GetSection("JwtConfig");
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = jwtSettings["Audience"],
                    ValidIssuer = jwtSettings["Issuer"], 
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                };
            });
            services.AddAuthorization();
            #endregion

            //Global Exception Handling
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            return services;
        }
    }
}
