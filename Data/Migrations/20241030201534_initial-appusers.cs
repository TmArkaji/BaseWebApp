using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BaseWebApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class initialappusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "primerApellido",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "primerNombre",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "segundoApellido",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "segundoNombre",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "32a2145c-1e0d-4102-a4bf-043481824ceb", null, "ADMIN", "ADMIN" },
                    { "32a2b1b0-619e-4152-86e8-401db0259a12", null, "Gestor", "GESTOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "primerApellido", "primerNombre", "segundoApellido", "segundoNombre" },
                values: new object[] { "32afb49a-7489-4e92-bdef-6ffaae987a42", 0, "1fefd418-cfed-4153-9342-cde6c7e73f2d", "leonardo.jrm@gmail.com", true, false, null, "LEONARDO.JRM@GMAIL.COM", "LEONARDO.JRM@GMAIL.COM", "AQAAAAIAAYagAAAAEDN2w4sE3it2JlBNKgEtQPPiTYkf6MZUEj4bKkFgPb7Qk74EQBaFC8xpvWGholaQGw==", null, false, "8767d4dc-2575-463a-98a2-7fd674179c39", false, "leonardo.jrm@gmail.com", "Admin", "System", "", "" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "32a2145c-1e0d-4102-a4bf-043481824ceb", "32afb49a-7489-4e92-bdef-6ffaae987a42" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "32a2b1b0-619e-4152-86e8-401db0259a12");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "32a2145c-1e0d-4102-a4bf-043481824ceb", "32afb49a-7489-4e92-bdef-6ffaae987a42" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "32a2145c-1e0d-4102-a4bf-043481824ceb");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "32afb49a-7489-4e92-bdef-6ffaae987a42");

            migrationBuilder.DropColumn(
                name: "primerApellido",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "primerNombre",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "segundoApellido",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "segundoNombre",
                table: "AspNetUsers");
        }
    }
}
