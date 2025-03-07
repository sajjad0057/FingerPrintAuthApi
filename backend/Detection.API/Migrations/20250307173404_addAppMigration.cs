using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Detection.API.Migrations
{
    /// <inheritdoc />
    public partial class addAppMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fingerprint",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "DeviceFingerprints",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "DeviceFingerprints");

            migrationBuilder.AddColumn<string>(
                name: "Fingerprint",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
