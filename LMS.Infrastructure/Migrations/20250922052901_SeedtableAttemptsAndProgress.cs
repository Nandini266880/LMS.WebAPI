using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedtableAttemptsAndProgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExamAttempts",
                columns: table => new
                {
                    AttemptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    AttemptedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamAttempts", x => x.AttemptId);
                    table.ForeignKey(
                        name: "FK_ExamAttempts_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamAttempts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Progresses",
                columns: table => new
                {
                    ProgressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progresses", x => x.ProgressId);
                    table.ForeignKey(
                        name: "FK_Progresses_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Progresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ExamAttempts",
                columns: new[] { "AttemptId", "AttemptedAt", "ExamId", "Score", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 5, 10, 0, 0, 0, DateTimeKind.Utc), 1, 85, 1 },
                    { 2, new DateTime(2025, 1, 6, 11, 30, 0, 0, DateTimeKind.Utc), 1, 72, 2 },
                    { 3, new DateTime(2025, 1, 8, 14, 0, 0, 0, DateTimeKind.Utc), 2, 90, 1 },
                    { 4, new DateTime(2025, 1, 9, 16, 0, 0, 0, DateTimeKind.Utc), 2, 60, 3 },
                    { 5, new DateTime(2025, 1, 10, 18, 0, 0, 0, DateTimeKind.Utc), 3, 77, 2 }
                });

            migrationBuilder.InsertData(
                table: "Progresses",
                columns: new[] { "ProgressId", "CompletedAt", "IsCompleted", "LessonId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 10, 12, 0, 0, 0, DateTimeKind.Utc), true, 1, 1 },
                    { 2, null, false, 2, 1 },
                    { 3, new DateTime(2025, 1, 12, 15, 30, 0, 0, DateTimeKind.Utc), true, 3, 2 },
                    { 4, new DateTime(2025, 1, 15, 9, 0, 0, 0, DateTimeKind.Utc), true, 4, 3 },
                    { 5, null, false, 5, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamAttempts_ExamId",
                table: "ExamAttempts",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamAttempts_UserId",
                table: "ExamAttempts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Progresses_LessonId",
                table: "Progresses",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Progresses_UserId",
                table: "Progresses",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamAttempts");

            migrationBuilder.DropTable(
                name: "Progresses");
        }
    }
}
