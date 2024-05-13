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

internal class Program
{
    static List<Recipe> recipes = new List<Recipe>();

    private static void Main(string[] args)
    {
        bool running = true;
        while (running)
        {
            //Display on command
            Console.WriteLine("1. Enter recipe details");
            Console.WriteLine("2. View full recipe");
            Console.WriteLine("3. Scale recipe");
            Console.WriteLine("4. Reset quantities to original values");
            Console.WriteLine("5. Clear all data to enter a new recipe");
            Console.WriteLine("6. Exit");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine();

            // the following switch represent each method function.
            switch (choice)
            {
                case "1":
                    AddRecipe();
                    break;
                case "2":
                    DisplayRecipes();
                    break;
                case "3":
                    ScaleRecipe();
                    break;
                case "4":
                    ResetQuantities();
                    Console.WriteLine("Quantities reset to original values.");
                    break;
                case "5":
                    ClearData();
                    Console.WriteLine("All data cleared. Enter a new recipe.");
                    break;
                case "6":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void AddRecipe()
    {
        Console.WriteLine("Enter recipe name:");
        string recipeName = Console.ReadLine();
        Recipe recipe = new Recipe(recipeName);

        Console.WriteLine("Enter the number of ingredients:");
        int number_ingredients = Convert.ToInt32(Console.ReadLine());

        for (int i = 0; i < number_ingredients; i++)
        {
            Console.WriteLine($"Enter ingredient {i + 1} details:");
            Console.Write("Name: ");
            string ingredientName = Console.ReadLine();
            Console.Write("Calories: ");
            int calories = Convert.ToInt32(Console.ReadLine());
            Console.Write("Food Type: ");
            string foodType = Console.ReadLine();

            Ingredient ingredient = new Ingredient { Name = ingredientName, Calories = calories, FoodType = foodType };
            recipe.Ingredients.Add(ingredient);
        }

        recipes.Add(recipe);

        int totalCalories = recipe.CalculateTotalCalories();
        if (totalCalories > 300)
        {
            Console.WriteLine($"Warning: {recipeName} exceeds 300 calories with {totalCalories} calories!");
        }
    }

    static void DisplayRecipes()
    {
        Console.WriteLine("All recipes:");
        foreach (var recipe in recipes)
        {
            Console.WriteLine($"Recipe: {recipe.Name}, Total Calories: {recipe.CalculateTotalCalories()}");
        }
    }

    static void ScaleRecipe()
    {
        Console.WriteLine("Enter the name of the recipe to scale:");
        string recipeName = Console.ReadLine();
        Recipe recipeToScale = recipes.Find(r => r.Name == recipeName);
        if (recipeToScale == null)
        {
            Console.WriteLine("Recipe not found.");
            return;
        }

        Console.Write("Enter scaling factor (0.5, 2, 3, etc.): ");
        double scaleFactor = Convert.ToDouble(Console.ReadLine());
        foreach (var ingredient in recipeToScale.Ingredients)
        {
            ingredient.Calories = (int)(ingredient.Calories * scaleFactor);
        }

        Console.WriteLine($"\nScaled Recipe '{recipeName}' (factor: {scaleFactor}):");
        foreach (var ingredient in recipeToScale.Ingredients)
        {
            Console.WriteLine($"Ingredient: {ingredient.Name}, Calories: {ingredient.Calories}, Food Type: {ingredient.FoodType}");
        }
    }

    static void ResetQuantities()
    {
        // Not implemented as it's not applicable in this context.
    }

    static void ClearData()
    {
        recipes.Clear();
    }
}