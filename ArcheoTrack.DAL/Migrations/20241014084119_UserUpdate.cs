using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArcheoTrack.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UserUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "Nickname");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_NoteUsers_NoteId",
                table: "NoteUsers",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteUsers_UserId",
                table: "NoteUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteUsers_Notes_NoteId",
                table: "NoteUsers",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NoteUsers_Users_UserId",
                table: "NoteUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteUsers_Notes_NoteId",
                table: "NoteUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_NoteUsers_Users_UserId",
                table: "NoteUsers");

            migrationBuilder.DropIndex(
                name: "IX_NoteUsers_NoteId",
                table: "NoteUsers");

            migrationBuilder.DropIndex(
                name: "IX_NoteUsers_UserId",
                table: "NoteUsers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Nickname",
                table: "Users",
                newName: "Username");
        }
    }
}
