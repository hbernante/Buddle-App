using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Buddle.Migrations
{
    /// <inheritdoc />
    public partial class ProfileImageAddV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ProfileImageData",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImageData",
                table: "AspNetUsers");
        }
    }
}
