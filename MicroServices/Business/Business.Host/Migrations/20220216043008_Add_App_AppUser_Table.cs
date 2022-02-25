using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Business.Migrations
{
    public partial class Add_App_AppUser_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Apps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AppId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    AppSecret = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    AppType = table.Column<int>(type: "int", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    OpenId = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Birth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AppId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUsers_Apps_AppId",
                        column: x => x.AppId,
                        principalTable: "Apps",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apps_AppId",
                table: "Apps",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_Apps_AppName",
                table: "Apps",
                column: "AppName");

            migrationBuilder.CreateIndex(
                name: "IX_Apps_AppType",
                table: "Apps",
                column: "AppType");

            migrationBuilder.CreateIndex(
                name: "IX_Apps_CreationTime",
                table: "Apps",
                column: "CreationTime");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_AppId",
                table: "AppUsers",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_City",
                table: "AppUsers",
                column: "City");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_CreationTime",
                table: "AppUsers",
                column: "CreationTime");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_Gender",
                table: "AppUsers",
                column: "Gender");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_Name",
                table: "AppUsers",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_OpenId",
                table: "AppUsers",
                column: "OpenId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_Phone",
                table: "AppUsers",
                column: "Phone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "Apps");
        }
    }
}
