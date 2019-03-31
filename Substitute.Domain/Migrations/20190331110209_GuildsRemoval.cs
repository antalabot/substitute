using Microsoft.EntityFrameworkCore.Migrations;

namespace Substitute.Domain.Migrations
{
    public partial class GuildsRemoval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuildRoles_Guilds_GuildId",
                table: "GuildRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_ImageReponses_Guilds_GuildId",
                table: "ImageReponses");

            migrationBuilder.DropTable(
                name: "Guilds");

            migrationBuilder.DropIndex(
                name: "IX_ImageReponses_GuildId",
                table: "ImageReponses");

            migrationBuilder.DropIndex(
                name: "IX_GuildRoles_GuildId",
                table: "GuildRoles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Guilds",
                columns: table => new
                {
                    Id = table.Column<decimal>(nullable: false),
                    IconUrl = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    OwnerId = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guilds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guilds_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageReponses_GuildId",
                table: "ImageReponses",
                column: "GuildId");

            migrationBuilder.CreateIndex(
                name: "IX_GuildRoles_GuildId",
                table: "GuildRoles",
                column: "GuildId");

            migrationBuilder.CreateIndex(
                name: "IX_Guilds_OwnerId",
                table: "Guilds",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_GuildRoles_Guilds_GuildId",
                table: "GuildRoles",
                column: "GuildId",
                principalTable: "Guilds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImageReponses_Guilds_GuildId",
                table: "ImageReponses",
                column: "GuildId",
                principalTable: "Guilds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
