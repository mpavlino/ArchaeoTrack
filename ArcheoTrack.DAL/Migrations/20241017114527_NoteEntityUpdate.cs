using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArcheoTrack.DAL.Migrations
{
    /// <inheritdoc />
    public partial class NoteEntityUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Notes");

            migrationBuilder.AddColumn<string>(
                name: "People",
                table: "Notes",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "People",
                table: "Notes");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Notes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
