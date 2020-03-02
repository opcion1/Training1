using Microsoft.EntityFrameworkCore.Migrations;

namespace Training1.Migrations
{
    public partial class MealFoodManyToMAny : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_Meal_MealId",
                table: "Food");

            migrationBuilder.DropIndex(
                name: "IX_Food_MealId",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "MealId",
                table: "Food");

            migrationBuilder.CreateTable(
                name: "MealFood",
                columns: table => new
                {
                    MealId = table.Column<int>(nullable: false),
                    FoodId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealFood", x => new { x.MealId, x.FoodId });
                    table.ForeignKey(
                        name: "FK_MealFood_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "FoodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealFood_Meal_MealId",
                        column: x => x.MealId,
                        principalTable: "Meal",
                        principalColumn: "MealId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MealFood_FoodId",
                table: "MealFood",
                column: "FoodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MealFood");

            migrationBuilder.AddColumn<int>(
                name: "MealId",
                table: "Food",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Food_MealId",
                table: "Food",
                column: "MealId");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Meal_MealId",
                table: "Food",
                column: "MealId",
                principalTable: "Meal",
                principalColumn: "MealId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
