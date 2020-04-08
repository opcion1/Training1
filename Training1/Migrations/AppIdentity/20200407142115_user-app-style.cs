using Microsoft.EntityFrameworkCore.Migrations;

namespace Training1.Migrations.AppIdentity
{
    public partial class userappstyle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppStyle",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppStyle",
                table: "AspNetUsers");
        }
    }
}
