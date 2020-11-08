using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class UserRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Houses_Users_UserId",
                table: "Houses");

            migrationBuilder.DropIndex(
                name: "IX_Houses_UserId",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Houses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Houses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Houses_UserId",
                table: "Houses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_Users_UserId",
                table: "Houses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
