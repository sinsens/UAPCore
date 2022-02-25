using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharedLocker.Migrations
{
    public partial class Init_SharedLocker_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SharedLocker_LockerRents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    RentTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ReturnTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReturnRemark = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AppId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_SharedLocker_LockerRents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SharedLocker_Lockers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    AppId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_SharedLocker_Lockers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SharedLocker_LockerRentLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LockerRentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedLocker_LockerRentLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SharedLocker_LockerRentLogs_SharedLocker_LockerRents_LockerRentId",
                        column: x => x.LockerRentId,
                        principalTable: "SharedLocker_LockerRents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SharedLocker_LockerRentInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LockerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedLocker_LockerRentInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SharedLocker_LockerRentInfos_SharedLocker_LockerRents_RentId",
                        column: x => x.RentId,
                        principalTable: "SharedLocker_LockerRents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SharedLocker_LockerRentInfos_SharedLocker_Lockers_LockerId",
                        column: x => x.LockerId,
                        principalTable: "SharedLocker_Lockers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SharedLocker_LockerRentInfos_LockerId",
                table: "SharedLocker_LockerRentInfos",
                column: "LockerId");

            migrationBuilder.CreateIndex(
                name: "IX_SharedLocker_LockerRentInfos_RentId",
                table: "SharedLocker_LockerRentInfos",
                column: "RentId");

            migrationBuilder.CreateIndex(
                name: "IX_SharedLocker_LockerRentLogs_LockerRentId",
                table: "SharedLocker_LockerRentLogs",
                column: "LockerRentId");

            migrationBuilder.CreateIndex(
                name: "IX_SharedLocker_LockerRents_AppId",
                table: "SharedLocker_LockerRents",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_SharedLocker_LockerRents_Name",
                table: "SharedLocker_LockerRents",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_SharedLocker_LockerRents_Phone",
                table: "SharedLocker_LockerRents",
                column: "Phone");

            migrationBuilder.CreateIndex(
                name: "IX_SharedLocker_LockerRents_RentTime",
                table: "SharedLocker_LockerRents",
                column: "RentTime");

            migrationBuilder.CreateIndex(
                name: "IX_SharedLocker_LockerRents_ReturnTime",
                table: "SharedLocker_LockerRents",
                column: "ReturnTime");

            migrationBuilder.CreateIndex(
                name: "IX_SharedLocker_LockerRents_Status",
                table: "SharedLocker_LockerRents",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_SharedLocker_Lockers_AppId",
                table: "SharedLocker_Lockers",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_SharedLocker_Lockers_IsActive",
                table: "SharedLocker_Lockers",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_SharedLocker_Lockers_Name",
                table: "SharedLocker_Lockers",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_SharedLocker_Lockers_Number",
                table: "SharedLocker_Lockers",
                column: "Number");

            migrationBuilder.CreateIndex(
                name: "IX_SharedLocker_Lockers_Status",
                table: "SharedLocker_Lockers",
                column: "Status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SharedLocker_LockerRentInfos");

            migrationBuilder.DropTable(
                name: "SharedLocker_LockerRentLogs");

            migrationBuilder.DropTable(
                name: "SharedLocker_Lockers");

            migrationBuilder.DropTable(
                name: "SharedLocker_LockerRents");
        }
    }
}
