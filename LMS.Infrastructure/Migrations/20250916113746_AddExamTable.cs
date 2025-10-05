using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddExamTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TotalMarks = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exams_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Exams",
                columns: new[] { "Id", "CourseId", "CreatedAt", "Title", "TotalMarks" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "C# Basics Quiz", 50 },
                    { 2, 2, new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "JavaScript ES6 Test", 60 },
                    { 3, 3, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Python Data Science Final", 100 },
                    { 4, 4, new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "React Components Midterm", 80 },
                    { 5, 5, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "SQL Fundamentals Quiz", 40 },
                    { 6, 2, new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "JS Mid Term", 90 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exams_CourseId",
                table: "Exams",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exams");
        }
    }
}
