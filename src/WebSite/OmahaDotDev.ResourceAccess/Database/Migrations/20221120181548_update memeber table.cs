using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OmahaDotDev.ResourceAccess.Database.Migrations
{
    /// <inheritdoc />
    public partial class updatememebertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSiteAdmin",
                schema: "groups",
                table: "Members",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                schema: "groups",
                table: "Members",
                keyColumn: "UserId",
                keyValue: "1",
                column: "IsSiteAdmin",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSiteAdmin",
                schema: "groups",
                table: "Members");
        }
    }
}
