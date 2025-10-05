using FluentValidation;
using LMS.Application.DTOs.Course;
using LMS.Application.Services.IServices;
using LMS.Domain.Enums;
using LMS.WebAPI.Extensions;
using LMS.WebAPI.Filters;
using LMS.WebAPI.Helpers;
using LMS.WebAPI.RateLimit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//Configure Serilog with ILogger
builder.Host.UseSerilog((context, services, loggerConfiguration) => loggerConfiguration
    .Enrich.FromLogContext()
    .WriteTo.File("logs/api-.log", rollingInterval: RollingInterval.Day)
);

//Custom DI
builder.Services.AddLMSDI(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Convert enums to their string representation
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    }); 

//Fluent Validation
builder.Services.AddValidatorsFromAssemblyContaining<CourseDtoValidation>();

//In-memory Cache
builder.Services.AddMemoryCache();

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    app.UseSwaggerUI(options =>
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<IpRateLimitMiddleware>();

app.MapControllers();

#region Minimal APIs - Enrollment
app.MapGet("api/enrollments/course/{courseId:int}", (int courseId) =>
{
    return Results.Ok(new { Message = $"Welcome, you have access to this course {courseId} !" });

}).AddEndpointFilter<EnrollmentFilter>()
  .RequireAuthorization();

app.MapPost("api/enrollments/", (int courseId, HttpContext httpContext, IEnrollmentService enrollmentService) =>
{
    try
    {
        var userIdClaim = httpContext.User.FindFirst("Id")?.Value;
        if (string.IsNullOrEmpty(userIdClaim))
        {
            throw new UnauthorizedAccessException("User ID claim is missing in the token.");
        }
        int userId = Convert.ToInt32(userIdClaim);

        enrollmentService.EnrollStudentAsync(userId, courseId);
        return Results.Ok(new { Message = $"You have been successfully registered in the {courseId}th course." });
    }
    catch (InvalidOperationException ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapPut("api/enrollments/{id:int}", async (int id, EnrollStatus Status, HttpContext httpContext, IEnrollmentService enrollmentService) =>
{
    int userId = httpContext.User.GetUserId();
    var enrollment = await enrollmentService.GetEnrollmentById(id);
    if (enrollment.UserId != userId)
        return Results.Forbid();

    await enrollmentService.UpdateEnrollmentStatusAsync(id, Status);
    return Results.Ok(new { Message = "Enrollment status updated successfully" });
});


#endregion

await app.RunAsync();
