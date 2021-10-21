using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArchitecture.Infrastructure.Persistence.Migrations
{
    public partial class addedcustomauditforsalesrecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "SalesRecord");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "SalesRecord",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "SalesRecord",
                newName: "EditedOn");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "SalesRecord",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "SalesRecord");

            migrationBuilder.RenameColumn(
                name: "EditedOn",
                table: "SalesRecord",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "SalesRecord",
                newName: "LastModifiedBy");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "SalesRecord",
                type: "datetime2",
                nullable: true);
        }
    }
}
