using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KafeRest.Migrations
{
    /// <inheritdoc />
    public partial class AddReserv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Categories_CategoryId",
                table: "Menu");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Menu",
                table: "Menu");

            migrationBuilder.RenameTable(
                name: "Menu",
                newName: "Yemekler");

            migrationBuilder.RenameIndex(
                name: "IX_Menu_CategoryId",
                table: "Yemekler",
                newName: "IX_Yemekler_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Yemekler",
                table: "Yemekler",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Clock = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    History = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Yemekler_Categories_CategoryId",
                table: "Yemekler",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Yemekler_Categories_CategoryId",
                table: "Yemekler");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Yemekler",
                table: "Yemekler");

            migrationBuilder.RenameTable(
                name: "Yemekler",
                newName: "Menu");

            migrationBuilder.RenameIndex(
                name: "IX_Yemekler_CategoryId",
                table: "Menu",
                newName: "IX_Menu_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Menu",
                table: "Menu",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Categories_CategoryId",
                table: "Menu",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
