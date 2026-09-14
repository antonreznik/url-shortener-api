using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ShortCode",
                table: "UrlShorteners",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "OriginalUrl",
                table: "UrlShorteners",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UrlShorteners_OriginalUrl",
                table: "UrlShorteners",
                column: "OriginalUrl");

            migrationBuilder.CreateIndex(
                name: "IX_UrlShorteners_ShortCode",
                table: "UrlShorteners",
                column: "ShortCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_UrlShorteners_OriginalUrl",
                table: "UrlShorteners");

            migrationBuilder.DropIndex(
                name: "IX_UrlShorteners_ShortCode",
                table: "UrlShorteners");

            migrationBuilder.AlterColumn<string>(
                name: "ShortCode",
                table: "UrlShorteners",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "OriginalUrl",
                table: "UrlShorteners",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
