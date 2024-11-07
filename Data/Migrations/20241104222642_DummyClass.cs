using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseWebApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class DummyClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DummyClassType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassTypeOrder = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DummyClassType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DummyClass",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Dummy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateField = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTimeField = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NulableDateField = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DecimalField = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IntField = table.Column<int>(type: "int", nullable: false),
                    TextAreaField = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoolField = table.Column<bool>(type: "bit", nullable: false),
                    DummyClassTypeID = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DummyClass", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DummyClass_DummyClassType_DummyClassTypeID",
                        column: x => x.DummyClassTypeID,
                        principalTable: "DummyClassType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "32afb49a-7489-4e92-bdef-6ffaae987a42",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "61d4dda8-7ed9-4df8-8ddc-cdb95d04fb7a", "AQAAAAIAAYagAAAAEAYIEUf0RMwdlQuLLqA0kVx7JbJDLh8Mdoaxko6wbhqR2rf0RJ1ja3wpGEb4epOnbw==", "1c7c9f08-2417-49a9-9dde-cd50fe9e7747" });

            migrationBuilder.CreateIndex(
                name: "IX_DummyClass_DummyClassTypeID",
                table: "DummyClass",
                column: "DummyClassTypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DummyClass");

            migrationBuilder.DropTable(
                name: "DummyClassType");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "32afb49a-7489-4e92-bdef-6ffaae987a42",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "26d447a5-c4ce-4861-90e4-b46b5f18f89e", "AQAAAAIAAYagAAAAEN5pSL3EdWow0BOxgik3NdgOiHV223BhS17apI2HyCyfQfLLv/ZnYNGVJegaTGAL+A==", "cededd9f-81b6-41ec-8fe5-3ee55364c57b" });
        }
    }
}
