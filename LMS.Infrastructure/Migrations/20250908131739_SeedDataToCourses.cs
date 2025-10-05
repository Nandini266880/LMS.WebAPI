using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataToCourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "courses",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "Price", "ThumbnailUrl", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Learn the basics of C# programming language and object-oriented concepts.", 49.990000000000002, "https://example.com/thumb1.jpg", "Introduction to C#", null },
                    { 2, new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Deep dive into JavaScript, including ES6+, asynchronous programming, and modern frameworks.", 59.990000000000002, "https://example.com/thumb2.jpg", "Advanced JavaScript", null },
                    { 3, new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Use Python to analyze data, visualize insights, and apply machine learning models.", 69.989999999999995, "https://example.com/thumb3.jpg", "Python for Data Science", null },
                    { 4, new DateTime(2025, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Build dynamic web applications using React, Redux, and modern frontend tooling.", 79.989999999999995, "https://example.com/thumb4.jpg", "Web Development with React", null },
                    { 5, new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Learn how to query, manipulate, and manage data using SQL databases.", 39.990000000000002, "https://example.com/thumb5.jpg", "SQL for Beginners", null },
                    { 6, new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Create robust web applications using ASP.NET Core MVC framework.", 59.990000000000002, "https://example.com/thumb6.jpg", "ASP.NET Core MVC", null },
                    { 7, new DateTime(2025, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Understand fundamental concepts of machine learning and build predictive models.", 89.989999999999995, "https://example.com/thumb7.jpg", "Machine Learning Basics", null },
                    { 8, new DateTime(2025, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Master HTML and CSS to design beautiful and responsive web pages.", 29.989999999999998, "https://example.com/thumb8.jpg", "Frontend with HTML & CSS", null },
                    { 9, new DateTime(2025, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Learn Java programming, object-oriented principles, and application development.", 49.990000000000002, "https://example.com/thumb9.jpg", "Java Fundamentals", null },
                    { 10, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Build scalable backend applications using Node.js and Express framework.", 59.990000000000002, "https://example.com/thumb10.jpg", "Node.js & Express", null },
                    { 11, new DateTime(2025, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Create 2D and 3D games using Unity engine and C# scripting.", 79.989999999999995, "https://example.com/thumb30.jpg", "Game Development with Unity", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "courses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "courses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "courses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "courses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "courses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "courses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "courses",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "courses",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "courses",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "courses",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "courses",
                keyColumn: "Id",
                keyValue: 11);
        }
    }
}
