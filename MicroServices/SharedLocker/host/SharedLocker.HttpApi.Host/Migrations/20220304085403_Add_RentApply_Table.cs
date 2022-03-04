using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharedLocker.Migrations
{
    public partial class Add_RentApply_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SharedLocker_LockerRentApplies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PinyinName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FullPinyinName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Count = table.Column<int>(type: "int", nullable: false),
                    RentTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuditTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Auditor = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AuditRemark = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DiscardReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscardTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LockerRentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_SharedLocker_LockerRentApplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SharedLocker_LockerRentApplies_SharedLocker_LockerRents_LockerRentId",
                        column: x => x.LockerRentId,
                        principalTable: "SharedLocker_LockerRents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SharedLocker_Lockers_IsDeleted",
                table: "SharedLocker_Lockers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SharedLocker_LockerRents_IsDeleted",
                table: "SharedLocker_LockerRents",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SharedLocker_LockerRentApplies_AppId",
                table: "SharedLocker_LockerRentApplies",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_SharedLocker_LockerRentApplies_IsDeleted",
                table: "SharedLocker_LockerRentApplies",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SharedLocker_LockerRentApplies_LockerRentId",
                table: "SharedLocker_LockerRentApplies",
                column: "LockerRentId");

            migrationBuilder.CreateIndex(
                name: "IX_SharedLocker_LockerRentApplies_Name",
                table: "SharedLocker_LockerRentApplies",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_SharedLocker_LockerRentApplies_Phone",
                table: "SharedLocker_LockerRentApplies",
                column: "Phone");

            migrationBuilder.CreateIndex(
                name: "IX_SharedLocker_LockerRentApplies_PinyinName",
                table: "SharedLocker_LockerRentApplies",
                column: "PinyinName");

            migrationBuilder.CreateIndex(
                name: "IX_SharedLocker_LockerRentApplies_RentTime",
                table: "SharedLocker_LockerRentApplies",
                column: "RentTime");

            migrationBuilder.CreateIndex(
                name: "IX_SharedLocker_LockerRentApplies_Status",
                table: "SharedLocker_LockerRentApplies",
                column: "Status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SharedLocker_LockerRentApplies");

            migrationBuilder.DropIndex(
                name: "IX_SharedLocker_Lockers_IsDeleted",
                table: "SharedLocker_Lockers");

            migrationBuilder.DropIndex(
                name: "IX_SharedLocker_LockerRents_IsDeleted",
                table: "SharedLocker_LockerRents");
        }
    }
}
