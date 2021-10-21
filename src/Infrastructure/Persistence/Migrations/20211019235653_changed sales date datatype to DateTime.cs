using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArchitecture.Infrastructure.Persistence.Migrations
{
    public partial class changedsalesdatedatatypetoDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "SalesRecord");

            migrationBuilder.AddColumn<DateTime>(
                name: "SalesDate",
                table: "SalesRecord",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalesDate",
                table: "SalesRecord");

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "SalesRecord",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
