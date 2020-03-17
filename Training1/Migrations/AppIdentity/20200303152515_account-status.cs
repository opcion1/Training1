using Microsoft.EntityFrameworkCore.Migrations;

namespace Training1.Migrations.AppIdentity
{
    public partial class accountstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "AccountStatus",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountStatus",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }
    }
}
