using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharedLocker.Migrations
{
    public partial class Add_Pinyin_Support_For_Rents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullPinyinName",
                table: "SharedLocker_LockerRents",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PinyinName",
                table: "SharedLocker_LockerRents",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SharedLocker_LockerRents_PinyinName",
                table: "SharedLocker_LockerRents",
                column: "PinyinName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SharedLocker_LockerRents_PinyinName",
                table: "SharedLocker_LockerRents");

            migrationBuilder.DropColumn(
                name: "FullPinyinName",
                table: "SharedLocker_LockerRents");

            migrationBuilder.DropColumn(
                name: "PinyinName",
                table: "SharedLocker_LockerRents");
        }
    }
}
