using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArchitecture.Infrastructure.Persistence.Migrations
{
    public partial class updatedcreatedonandeditedon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByWho",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditedByWho",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByWho",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "EditedByWho",
                table: "Items");
        }
    }
}
