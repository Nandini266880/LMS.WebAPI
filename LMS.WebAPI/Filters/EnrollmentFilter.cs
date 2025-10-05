using LMS.Application.Services.IServices;
using LMS.WebAPI.Extensions;

namespace LMS.WebAPI.Filters
{
    public class EnrollmentFilter : IEndpointFilter
    {
        private readonly IEnrollmentService _enrollmentService;
        public EnrollmentFilter(IEnrollmentService enrollmentService)
        {
           _enrollmentService = enrollmentService;
        }
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var httpContext = context.HttpContext;
            var courseId = context.GetArgument<int>(0);

            int userId = httpContext.User.GetUserId();

            bool hasAccess = await _enrollmentService.HasEnrolledInCourse(userId, courseId);

            if (!hasAccess)
            {
                return Results.Forbid();
            }

            return await next(context);
        }
    }
}
