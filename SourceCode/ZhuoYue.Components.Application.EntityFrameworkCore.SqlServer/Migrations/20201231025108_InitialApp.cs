using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZhuoYue.Components.Application.EntityFrameworkCore.SqlServer.Migrations
{
    public partial class InitialApp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "App",
                columns: table => new
                {
                    AppId = table.Column<string>(maxLength: 36, nullable: false),
                    CreatedUserId = table.Column<string>(maxLength: 256, nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    LastUpdatedUserId = table.Column<string>(maxLength: 256, nullable: true),
                    LastUpdatedTime = table.Column<DateTime>(nullable: true),
                    AppName = table.Column<string>(maxLength: 256, nullable: true),
                    AppRemarks = table.Column<string>(maxLength: 4096, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App", x => x.AppId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "App");
        }
    }
}
