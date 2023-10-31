using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ispit.Todo.Data.Migrations
{
    /// <inheritdoc />
    public partial class DodaniDbSetovi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Todolist_TodolistId",
                table: "Task");

            migrationBuilder.DropForeignKey(
                name: "FK_Todolist_AspNetUsers_UserId",
                table: "Todolist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Todolist",
                table: "Todolist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Task",
                table: "Task");

            migrationBuilder.RenameTable(
                name: "Todolist",
                newName: "Todolists");

            migrationBuilder.RenameTable(
                name: "Task",
                newName: "Tasks");

            migrationBuilder.RenameIndex(
                name: "IX_Todolist_UserId",
                table: "Todolists",
                newName: "IX_Todolists_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Task_TodolistId",
                table: "Tasks",
                newName: "IX_Tasks_TodolistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Todolists",
                table: "Todolists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Todolists_TodolistId",
                table: "Tasks",
                column: "TodolistId",
                principalTable: "Todolists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Todolists_AspNetUsers_UserId",
                table: "Todolists",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Todolists_TodolistId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Todolists_AspNetUsers_UserId",
                table: "Todolists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Todolists",
                table: "Todolists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.RenameTable(
                name: "Todolists",
                newName: "Todolist");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "Task");

            migrationBuilder.RenameIndex(
                name: "IX_Todolists_UserId",
                table: "Todolist",
                newName: "IX_Todolist_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_TodolistId",
                table: "Task",
                newName: "IX_Task_TodolistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Todolist",
                table: "Todolist",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Task",
                table: "Task",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Todolist_TodolistId",
                table: "Task",
                column: "TodolistId",
                principalTable: "Todolist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Todolist_AspNetUsers_UserId",
                table: "Todolist",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
