using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AssignmentTask.Data.Migrations
{
    public partial class CommentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<Guid>(nullable: false),
                    CommentArea = table.Column<string>(nullable: true),
                    StudentID = table.Column<Guid>(nullable: false),
                    TeacherID = table.Column<Guid>(nullable: false),
                    AssignmentID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_Assignments_AssignmentID",
                        column: x => x.AssignmentID,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Teachers_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AssignmentID",
                table: "Comments",
                column: "AssignmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_StudentID",
                table: "Comments",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_TeacherID",
                table: "Comments",
                column: "TeacherID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");
        }
    }
}
