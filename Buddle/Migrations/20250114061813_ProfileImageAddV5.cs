using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Buddle.Migrations
{
    /// <inheritdoc />
    public partial class ProfileImageAddV5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfileImageData",
                table: "AspNetUsers",
                newName: "ProfileImage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfileImage",
                table: "AspNetUsers",
                newName: "ProfileImageData");
        }
    }
}
