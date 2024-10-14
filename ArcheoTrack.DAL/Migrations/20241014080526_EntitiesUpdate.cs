using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArcheoTrack.DAL.Migrations
{
    /// <inheritdoc />
    public partial class EntitiesUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
