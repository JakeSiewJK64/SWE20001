using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArchitecture.Infrastructure.Persistence.Migrations
{
    public partial class addedenumforitemcategorytype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemCategory",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemCategory",
                table: "Items");
        }
    }
}
