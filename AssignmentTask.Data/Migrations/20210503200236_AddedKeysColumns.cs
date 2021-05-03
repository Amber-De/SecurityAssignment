using Microsoft.EntityFrameworkCore.Migrations;

namespace AssignmentTask.Data.Migrations
{
    public partial class AddedKeysColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PrivateKey",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicKey",
                table: "Students",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrivateKey",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PublicKey",
                table: "Students");
        }
    }
}
