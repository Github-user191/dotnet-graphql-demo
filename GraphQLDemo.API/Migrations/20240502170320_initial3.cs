using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraphQLDemo.API.Migrations
{
    public partial class initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseDtoStudentDto_Courses_CoursesId",
                table: "CourseDtoStudentDto");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseDtoStudentDto_Students_StudentsId",
                table: "CourseDtoStudentDto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseDtoStudentDto",
                table: "CourseDtoStudentDto");

            migrationBuilder.RenameTable(
                name: "CourseDtoStudentDto",
                newName: "StudentCourses");

            migrationBuilder.RenameIndex(
                name: "IX_CourseDtoStudentDto_StudentsId",
                table: "StudentCourses",
                newName: "IX_StudentCourses_StudentsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentCourses",
                table: "StudentCourses",
                columns: new[] { "CoursesId", "StudentsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_Courses_CoursesId",
                table: "StudentCourses",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_Students_StudentsId",
                table: "StudentCourses",
                column: "StudentsId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_Courses_CoursesId",
                table: "StudentCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_Students_StudentsId",
                table: "StudentCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentCourses",
                table: "StudentCourses");

            migrationBuilder.RenameTable(
                name: "StudentCourses",
                newName: "CourseDtoStudentDto");

            migrationBuilder.RenameIndex(
                name: "IX_StudentCourses_StudentsId",
                table: "CourseDtoStudentDto",
                newName: "IX_CourseDtoStudentDto_StudentsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseDtoStudentDto",
                table: "CourseDtoStudentDto",
                columns: new[] { "CoursesId", "StudentsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CourseDtoStudentDto_Courses_CoursesId",
                table: "CourseDtoStudentDto",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseDtoStudentDto_Students_StudentsId",
                table: "CourseDtoStudentDto",
                column: "StudentsId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
