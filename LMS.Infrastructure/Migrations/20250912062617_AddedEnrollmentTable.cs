using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedEnrollmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "enrollments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    EnrolledAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_enrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_enrollments_courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_enrollments_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "enrollments",
                columns: new[] { "Id", "CourseId", "EnrolledAt", "Status", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2 },
                    { 2, 5, new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2 },
                    { 3, 2, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3 },
                    { 4, 6, new DateTime(2025, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 3 },
                    { 5, 3, new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 4 },
                    { 6, 7, new DateTime(2025, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_enrollments_CourseId",
                table: "enrollments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_enrollments_UserId",
                table: "enrollments",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "enrollments");
        }
    }
}
