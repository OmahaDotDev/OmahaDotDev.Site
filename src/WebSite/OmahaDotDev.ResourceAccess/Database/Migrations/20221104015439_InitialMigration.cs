using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OmahaDotDev.ResourceAccess.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "groups");

            migrationBuilder.CreateTable(
                name: "Members",
                schema: "groups",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                schema: "groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Members_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalSchema: "groups",
                        principalTable: "Members",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Groups_Members_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalSchema: "groups",
                        principalTable: "Members",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupDomainNames",
                schema: "groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DomainName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupDomainNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupDomainNames_Groups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "groups",
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupDomainNames_Members_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalSchema: "groups",
                        principalTable: "Members",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupDomainNames_Members_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalSchema: "groups",
                        principalTable: "Members",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupDomainNames_CreatedByUserId",
                schema: "groups",
                table: "GroupDomainNames",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupDomainNames_GroupId",
                schema: "groups",
                table: "GroupDomainNames",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupDomainNames_UpdatedByUserId",
                schema: "groups",
                table: "GroupDomainNames",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CreatedByUserId",
                schema: "groups",
                table: "Groups",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_UpdatedByUserId",
                schema: "groups",
                table: "Groups",
                column: "UpdatedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupDomainNames",
                schema: "groups");

            migrationBuilder.DropTable(
                name: "Groups",
                schema: "groups");

            migrationBuilder.DropTable(
                name: "Members",
                schema: "groups");
        }
    }
}
