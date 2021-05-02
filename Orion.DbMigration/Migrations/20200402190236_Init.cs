using Microsoft.EntityFrameworkCore.Migrations;

namespace Orion.DbMigration.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IPLocations",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IPFrom = table.Column<float>(nullable: false),
                    IPTo = table.Column<float>(nullable: false),
                    CountryCode = table.Column<string>(maxLength: 2, nullable: false),
                    CountryName = table.Column<string>(maxLength: 64, nullable: false),
                    RegionName = table.Column<string>(maxLength: 128, nullable: false),
                    CityName = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPLocations", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IPLocations_IPFrom",
                table: "IPLocations",
                column: "IPFrom");

            migrationBuilder.CreateIndex(
                name: "IX_IPLocations_IPTo",
                table: "IPLocations",
                column: "IPTo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IPLocations");
        }
    }
}
