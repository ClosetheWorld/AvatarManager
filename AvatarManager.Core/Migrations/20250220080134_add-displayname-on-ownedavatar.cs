using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvatarManager.Core.Migrations
{
    /// <inheritdoc />
    public partial class adddisplaynameonownedavatar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "OwnedAvatars",
                type: "TEXT",
                nullable: true,
                defaultValue: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "OwnedAvatars");
        }
    }
}
