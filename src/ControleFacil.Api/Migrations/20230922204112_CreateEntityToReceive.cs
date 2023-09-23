using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleFacil.Api.Migrations
{
    /// <inheritdoc />
    public partial class CreateEntityToReceive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "toReceive",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReceivedValue = table.Column<decimal>(type: "decimal", nullable: false),
                    ReceivedDate = table.Column<DateTime>(type: "timestamp", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    NatureReleaseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR", nullable: false),
                    Details = table.Column<string>(type: "VARCHAR", nullable: true),
                    OriginalValue = table.Column<decimal>(type: "decimal", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    ReferenceDate = table.Column<DateTime>(type: "timestamp", nullable: true),
                    InactivationDate = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_toReceive", x => x.Id);
                    table.ForeignKey(
                        name: "FK_toReceive_naturerelease_NatureReleaseId",
                        column: x => x.NatureReleaseId,
                        principalTable: "naturerelease",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_toReceive_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_toReceive_NatureReleaseId",
                table: "toReceive",
                column: "NatureReleaseId");

            migrationBuilder.CreateIndex(
                name: "IX_toReceive_UserId",
                table: "toReceive",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "toReceive");
        }
    }
}
