using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoteApi.Migrations
{
    /// <inheritdoc />
    public partial class GuidChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Notes",
                newName: "guid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "guid",
                table: "Notes",
                newName: "ID");
        }
    }
}
