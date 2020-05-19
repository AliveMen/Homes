using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Homes.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    Levels = table.Column<int>(nullable: false),
                    FoundationYear = table.Column<int>(nullable: false),
                    City = table.Column<string>(maxLength: 30, nullable: false),
                    PostCode = table.Column<int>(nullable: false),
                    AddressLine = table.Column<string>(maxLength: 120, nullable: false),
                    Image = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    EndRentDate = table.Column<DateTime>(nullable: false),
                    BuildingId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tenant_Building_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Building",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_BuildingId",
                table: "Tenant",
                column: "BuildingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tenant");

            migrationBuilder.DropTable(
                name: "Building");
        }
    }
}
