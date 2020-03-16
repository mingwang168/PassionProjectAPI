using Microsoft.EntityFrameworkCore.Migrations;

namespace MemorizeWordsAPI.Migrations
{
    public partial class AddUserIDToSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "LearningSchedules",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "LearningSchedules",
                keyColumn: "ScheduleID",
                keyValue: 1,
                column: "UserName",
                value: "mingwang");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "LearningSchedules");
        }
    }
}
