using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShortenedUrl",
                table: "UrlShorteners",
                newName: "ShortCode");

            migrationBuilder.CreateSequence(
                name: "UrlIdSequence",
                startValue: 10000L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "UrlIdSequence");

            migrationBuilder.RenameColumn(
                name: "ShortCode",
                table: "UrlShorteners",
                newName: "ShortenedUrl");
        }
    }
}
