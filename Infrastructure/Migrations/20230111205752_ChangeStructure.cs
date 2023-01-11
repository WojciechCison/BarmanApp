using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "CoctailIngridients");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "Ingridients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Ingridients");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "CoctailIngridients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
