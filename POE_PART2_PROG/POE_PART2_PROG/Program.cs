using System;
using System.Collections.Generic;

// Define a delegate for notifying when a recipe exceeds 300 calories
public delegate void RecipeExceedsCaloriesHandler(string recipeName, int totalCalories);

// Define an ingredient class
public class Ingredient
{
    public string Name { get; set; }
    public int Calories { get; set; }
    public string FoodType { get; set; }
}

// Define a recipe class
public class Recipe
{
    public string Name { get; set; }
    public List<Ingredient> Ingredients { get; set; }

    public Recipe(string name)
    {
        Name = name;
        Ingredients = new List<Ingredient>();
    }

    // Method to calculate total calories of the recipe
    public int CalculateTotalCalories()
    {
        int totalCalories = 0;
        foreach (var ingredient in Ingredients)
        {
            totalCalories += ingredient.Calories;
        }
        return totalCalories;
    }
}

// MY Main program
class Program
{
    static void Main(string[] args)
    {
        List<Recipe> recipes = new List<Recipe>();

        // Function to notify when a recipe exceeds 300 calories
        RecipeExceedsCaloriesHandler notifyExceedsCalories = (recipeName, totalCalories) =>
        {
            if (totalCalories > 300)
            {
                Console.WriteLine($"Warning: {recipeName} exceeds 300 calories with {totalCalories} calories!");
            }
        };

        // Function to add a new recipe
        void AddRecipe()
        {
            Console.WriteLine("Enter recipe name:");
            string recipeName = Console.ReadLine();
            Recipe recipe = new Recipe(recipeName);

            Console.WriteLine("Enter ingredients for the recipe (press enter to stop adding ingredients):");
            string input;
            do
            {
                Console.WriteLine("Enter ingredient name:");
                string ingredientName = Console.ReadLine();
                Console.WriteLine("Enter calories for the ingredient:");
                int calories = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter food type for the ingredient:");
                string foodType = Console.ReadLine();

                Ingredient ingredient = new Ingredient { Name = ingredientName, Calories = calories, FoodType = foodType };
                recipe.Ingredients.Add(ingredient);

                Console.WriteLine("Do you want to add another ingredient? (yes/no)");
                input = Console.ReadLine();
            } while (input.ToLower() == "yes");

            recipes.Add(recipe);
            int totalCalories = recipe.CalculateTotalCalories();
            notifyExceedsCalories(recipe.Name, totalCalories);
        }

        // Function to display all recipes
        void DisplayRecipes()
        {
            Console.WriteLine("All recipes:");
            foreach (var recipe in recipes)
            {
                Console.WriteLine($"Recipe: {recipe.Name}, Total Calories: {recipe.CalculateTotalCalories()}");
            }
        }

        // Main menu
        string choice;
        do
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Add Recipe");
            Console.WriteLine("2. Display Recipes");
            Console.WriteLine("3. Exit");
            Console.WriteLine("Enter your choice:");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddRecipe();
                    break;
                case "2":
                    DisplayRecipes();
                    break;
                case "3":
                    Console.WriteLine("Exiting...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

        } while (choice != "3");
    }
}
