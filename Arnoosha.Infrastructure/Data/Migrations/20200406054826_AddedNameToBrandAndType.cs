using Microsoft.EntityFrameworkCore.Migrations;

namespace Arnoosha.Infrastructure.Data.Migrations
{
    public partial class AddedNameToBrandAndType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProductTypes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProductBrands",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProductBrands");
        }
    }
}
