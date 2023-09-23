using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleFacil.Api.Migrations
{
    /// <inheritdoc />
    public partial class CreateEntityNatureRelease : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "naturerelease",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR", nullable: false),
                    Details = table.Column<string>(type: "VARCHAR", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    InactivationDate = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_naturerelease", x => x.Id);
                    table.ForeignKey(
                        name: "FK_naturerelease_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_naturerelease_UserId",
                table: "naturerelease",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "naturerelease");
        }
    }
}
