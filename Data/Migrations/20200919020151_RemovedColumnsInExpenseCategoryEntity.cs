using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class RemovedColumnsInExpenseCategoryEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseCategories_Users_CreatorId",
                table: "ExpenseCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseCategories_Users_LastModifierId",
                table: "ExpenseCategories");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseCategories_CreatorId",
                table: "ExpenseCategories");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseCategories_LastModifierId",
                table: "ExpenseCategories");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "ExpenseCategories");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "ExpenseCategories");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "ExpenseCategories");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "ExpenseCategories");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ExpenseCategories",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ExpenseCategories");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "ExpenseCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "ExpenseCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "ExpenseCategories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastModifierId",
                table: "ExpenseCategories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseCategories_CreatorId",
                table: "ExpenseCategories",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseCategories_LastModifierId",
                table: "ExpenseCategories",
                column: "LastModifierId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseCategories_Users_CreatorId",
                table: "ExpenseCategories",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseCategories_Users_LastModifierId",
                table: "ExpenseCategories",
                column: "LastModifierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
