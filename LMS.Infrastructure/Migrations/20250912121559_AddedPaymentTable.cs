using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedPaymentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courses_users_CreatedBy",
                table: "courses");

            migrationBuilder.DropForeignKey(
                name: "FK_enrollments_courses_CourseId",
                table: "enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_enrollments_users_UserId",
                table: "enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_lessons_courses_CourseId",
                table: "lessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_lessons",
                table: "lessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_enrollments",
                table: "enrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_courses",
                table: "courses");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "lessons",
                newName: "Lessons");

            migrationBuilder.RenameTable(
                name: "enrollments",
                newName: "Enrollments");

            migrationBuilder.RenameTable(
                name: "courses",
                newName: "Courses");

            migrationBuilder.RenameIndex(
                name: "IX_users_Email",
                table: "Users",
                newName: "IX_Users_Email");

            migrationBuilder.RenameIndex(
                name: "IX_lessons_CourseId_LessonIdx",
                table: "Lessons",
                newName: "IX_Lessons_CourseId_LessonIdx");

            migrationBuilder.RenameIndex(
                name: "IX_enrollments_UserId",
                table: "Enrollments",
                newName: "IX_Enrollments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_enrollments_CourseId",
                table: "Enrollments",
                newName: "IX_Enrollments_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_courses_CreatedBy",
                table: "Courses",
                newName: "IX_Courses_CreatedBy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                table: "Courses",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CourseId",
                table: "Payments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserId",
                table: "Payments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Users_CreatedBy",
                table: "Courses",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Courses_CourseId",
                table: "Enrollments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Users_UserId",
                table: "Enrollments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Courses_CourseId",
                table: "Lessons",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Users_CreatedBy",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Courses_CourseId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Users_UserId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Courses_CourseId",
                table: "Lessons");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Lessons",
                newName: "lessons");

            migrationBuilder.RenameTable(
                name: "Enrollments",
                newName: "enrollments");

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "courses");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Email",
                table: "users",
                newName: "IX_users_Email");

            migrationBuilder.RenameIndex(
                name: "IX_Lessons_CourseId_LessonIdx",
                table: "lessons",
                newName: "IX_lessons_CourseId_LessonIdx");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_UserId",
                table: "enrollments",
                newName: "IX_enrollments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_CourseId",
                table: "enrollments",
                newName: "IX_enrollments_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_CreatedBy",
                table: "courses",
                newName: "IX_courses_CreatedBy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_lessons",
                table: "lessons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_enrollments",
                table: "enrollments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_courses",
                table: "courses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_courses_users_CreatedBy",
                table: "courses",
                column: "CreatedBy",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_enrollments_courses_CourseId",
                table: "enrollments",
                column: "CourseId",
                principalTable: "courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_enrollments_users_UserId",
                table: "enrollments",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_lessons_courses_CourseId",
                table: "lessons",
                column: "CourseId",
                principalTable: "courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
