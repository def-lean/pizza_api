using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaStore.Migrations
{
    /// <inheritdoc />
    public partial class AddManyToMannnyPizzaIngredients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientPizza_Ingredient_IngredientsId",
                table: "IngredientPizza");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientPizza_Pizzas_PizzasId",
                table: "IngredientPizza");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IngredientPizza",
                table: "IngredientPizza");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient");

            migrationBuilder.RenameTable(
                name: "IngredientPizza",
                newName: "PizzaIngredients");

            migrationBuilder.RenameTable(
                name: "Ingredient",
                newName: "Ingredients");

            migrationBuilder.RenameIndex(
                name: "IX_IngredientPizza_PizzasId",
                table: "PizzaIngredients",
                newName: "IX_PizzaIngredients_PizzasId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PizzaIngredients",
                table: "PizzaIngredients",
                columns: new[] { "IngredientsId", "PizzasId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PizzaIngredients_Ingredients_IngredientsId",
                table: "PizzaIngredients",
                column: "IngredientsId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PizzaIngredients_Pizzas_PizzasId",
                table: "PizzaIngredients",
                column: "PizzasId",
                principalTable: "Pizzas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PizzaIngredients_Ingredients_IngredientsId",
                table: "PizzaIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_PizzaIngredients_Pizzas_PizzasId",
                table: "PizzaIngredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PizzaIngredients",
                table: "PizzaIngredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients");

            migrationBuilder.RenameTable(
                name: "PizzaIngredients",
                newName: "IngredientPizza");

            migrationBuilder.RenameTable(
                name: "Ingredients",
                newName: "Ingredient");

            migrationBuilder.RenameIndex(
                name: "IX_PizzaIngredients_PizzasId",
                table: "IngredientPizza",
                newName: "IX_IngredientPizza_PizzasId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IngredientPizza",
                table: "IngredientPizza",
                columns: new[] { "IngredientsId", "PizzasId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientPizza_Ingredient_IngredientsId",
                table: "IngredientPizza",
                column: "IngredientsId",
                principalTable: "Ingredient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientPizza_Pizzas_PizzasId",
                table: "IngredientPizza",
                column: "PizzasId",
                principalTable: "Pizzas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
