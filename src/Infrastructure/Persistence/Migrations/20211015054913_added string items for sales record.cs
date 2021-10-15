using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArchitecture.Infrastructure.Persistence.Migrations
{
    public partial class addedstringitemsforsalesrecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Items",
                table: "SalesRecord",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Items",
                table: "SalesRecord");
        }
    }
}
