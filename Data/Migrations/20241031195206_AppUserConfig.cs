using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BaseWebApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class AppUserConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "segundoNombre",
                table: "AspNetUsers",
                newName: "SegundoNombre");

            migrationBuilder.RenameColumn(
                name: "segundoApellido",
                table: "AspNetUsers",
                newName: "SegundoApellido");

            migrationBuilder.RenameColumn(
                name: "primerNombre",
                table: "AspNetUsers",
                newName: "PrimerNombre");

            migrationBuilder.RenameColumn(
                name: "primerApellido",
                table: "AspNetUsers",
                newName: "PrimerApellido");

            migrationBuilder.CreateTable(
                name: "AppUserConfig",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    appUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserConfig", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AppUserConfig_AspNetUsers_appUserId",
                        column: x => x.appUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserConfig_appUserId",
                table: "AppUserConfig",
                column: "appUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserConfig");

            migrationBuilder.RenameColumn(
                name: "SegundoNombre",
                table: "AspNetUsers",
                newName: "segundoNombre");

            migrationBuilder.RenameColumn(
                name: "SegundoApellido",
                table: "AspNetUsers",
                newName: "segundoApellido");

            migrationBuilder.RenameColumn(
                name: "PrimerNombre",
                table: "AspNetUsers",
                newName: "primerNombre");

            migrationBuilder.RenameColumn(
                name: "PrimerApellido",
                table: "AspNetUsers",
                newName: "primerApellido");
        }
    }
}
