using Microsoft.EntityFrameworkCore.Migrations;

namespace Training1.Migrations
{
    public partial class FoodNumberOfPeople : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfPeople",
                table: "Food",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfPeople",
                table: "Food");
        }
    }
}
