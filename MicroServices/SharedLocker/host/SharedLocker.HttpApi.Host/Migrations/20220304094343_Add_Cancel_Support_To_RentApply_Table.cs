using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharedLocker.Migrations
{
    public partial class Add_Cancel_Support_To_RentApply_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DiscardReason",
                table: "SharedLocker_LockerRentApplies",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CancelReason",
                table: "SharedLocker_LockerRentApplies",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CancelTime",
                table: "SharedLocker_LockerRentApplies",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancelReason",
                table: "SharedLocker_LockerRentApplies");

            migrationBuilder.DropColumn(
                name: "CancelTime",
                table: "SharedLocker_LockerRentApplies");

            migrationBuilder.AlterColumn<string>(
                name: "DiscardReason",
                table: "SharedLocker_LockerRentApplies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);
        }
    }
}
