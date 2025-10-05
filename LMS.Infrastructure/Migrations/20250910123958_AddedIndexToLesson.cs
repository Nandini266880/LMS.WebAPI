using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedIndexToLesson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_lessons_CourseId",
                table: "lessons");

            migrationBuilder.CreateIndex(
                name: "IX_lessons_CourseId_LessonIdx",
                table: "lessons",
                columns: new[] { "CourseId", "LessonIdx" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_lessons_CourseId_LessonIdx",
                table: "lessons");

            migrationBuilder.CreateIndex(
                name: "IX_lessons_CourseId",
                table: "lessons",
                column: "CourseId");
        }
    }
}
