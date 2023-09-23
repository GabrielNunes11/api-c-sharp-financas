using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleFacil.Api.Migrations
{
    /// <inheritdoc />
    public partial class CreateEntityToPay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "toPay",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PaidValue = table.Column<decimal>(type: "decimal", nullable: false),
                    PaidDate = table.Column<DateTime>(type: "timestamp", nullable: true),
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
                    table.PrimaryKey("PK_toPay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_toPay_naturerelease_NatureReleaseId",
                        column: x => x.NatureReleaseId,
                        principalTable: "naturerelease",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_toPay_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_toPay_NatureReleaseId",
                table: "toPay",
                column: "NatureReleaseId");

            migrationBuilder.CreateIndex(
                name: "IX_toPay_UserId",
                table: "toPay",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "toPay");
        }
    }
}
