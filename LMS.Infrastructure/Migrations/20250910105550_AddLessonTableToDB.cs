using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLessonTableToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "lessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    LessonIdx = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lessons_courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "lessons",
                columns: new[] { "Id", "ContentUrl", "CourseId", "CreatedAt", "Duration", "LessonIdx", "Title" },
                values: new object[,]
                {
                    { 1, "", 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, 1, "Introduction to C# Environment" },
                    { 2, "", 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 45, 2, "Variables and Data Types" },
                    { 3, "", 2, new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 40, 1, "JavaScript ES6 Features" },
                    { 4, "", 2, new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 50, 2, "Asynchronous JavaScript" },
                    { 5, "", 3, new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 35, 1, "Python Basics" },
                    { 6, "", 3, new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 50, 2, "Data Analysis with Pandas" },
                    { 7, "", 4, new DateTime(2025, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 40, 1, "React Components" },
                    { 8, "", 4, new DateTime(2025, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 45, 2, "State and Props" },
                    { 9, "", 5, new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 30, 1, "Introduction to SQL" },
                    { 10, "", 5, new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 50, 2, "Advanced SQL Queries" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_lessons_CourseId",
                table: "lessons",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lessons");
        }
    }
}
