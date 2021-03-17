using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZhuoYue.Components.Code.EntityFrameworkCore.SqlServer.Migrations
{
    public partial class InitialCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CodeCategories",
                columns: table => new
                {
                    CategoryId = table.Column<string>(maxLength: 36, nullable: false),
                    CreatedUserId = table.Column<string>(maxLength: 256, nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    LastUpdatedUserId = table.Column<string>(maxLength: 256, nullable: true),
                    LastUpdatedTime = table.Column<DateTime>(nullable: true),
                    CategoryName = table.Column<string>(maxLength: 256, nullable: true),
                    Remarks = table.Column<string>(maxLength: 4096, nullable: true),
                    AppId = table.Column<string>(maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeCategories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "CodeItems",
                columns: table => new
                {
                    CodeId = table.Column<string>(maxLength: 36, nullable: false),
                    CreatedUserId = table.Column<string>(maxLength: 256, nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    LastUpdatedUserId = table.Column<string>(maxLength: 256, nullable: true),
                    LastUpdatedTime = table.Column<DateTime>(nullable: true),
                    CodeName = table.Column<string>(maxLength: 256, nullable: true),
                    Remarks = table.Column<string>(maxLength: 4096, nullable: true),
                    CodeValue = table.Column<string>(maxLength: 256, nullable: true),
                    Sequence = table.Column<int>(nullable: false),
                    CodeCategoryId = table.Column<string>(maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeItems", x => x.CodeId);
                    table.ForeignKey(
                        name: "FK_CodeItems_CodeCategories_CodeCategoryId",
                        column: x => x.CodeCategoryId,
                        principalTable: "CodeCategories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CodeItems_CodeCategoryId",
                table: "CodeItems",
                column: "CodeCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CodeItems");

            migrationBuilder.DropTable(
                name: "CodeCategories");
        }
    }
}
