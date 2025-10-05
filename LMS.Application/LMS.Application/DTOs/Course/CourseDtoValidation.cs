using FluentValidation;

namespace LMS.Application.DTOs.Course
{
    public class CourseDtoValidation : AbstractValidator<CourseCreateDto>
    {
        //References - https://medium.com/@lucas.and227/fluent-validation-with-net-core-da0d9da73c8a,
        //https://ravindradevrani.medium.com/fluent-validation-in-net-core-8-0-c748da274204
        public CourseDtoValidation()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Course Title is required.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .MinimumLength(10)
                .WithMessage("Description must be of 10 characters.");

            RuleFor(x => x.ThumbnailUrl)
                .NotEmpty()
                .WithMessage("Add thumbnail url.");
        }
    }
}
