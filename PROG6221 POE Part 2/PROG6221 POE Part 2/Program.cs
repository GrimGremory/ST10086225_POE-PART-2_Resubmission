using System;
using System.Collections.Generic;

namespace RecipeBook
{
    class Program
    {
        delegate void RecipeCaloriesExceededHandler(string recipeName, int calories);

        static void Main(string[] args)
        {
            SortedDictionary<string, Recipe> recipes = new SortedDictionary<string, Recipe>();
            SortedDictionary<string, Recipe> clonedRecipes = new SortedDictionary<string, Recipe>();

            while (true)
            {
                // display the menu options in blue
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Select an option: \n " +
                                  "1. Enter recipe \n " +
                                  "2. Display recipe \n " +
                                  "3. Scale recipe \n " +
                                  "4. Reset quantities " +
                                  "\n 5. Clear recipe " +
                                  "\n 6. Exit");
                // display the console text in white
                Console.ForegroundColor = ConsoleColor.White;
                int option;
                while (!int.TryParse(Console.ReadLine(), out option))
                {
                    ErrorMessage(); // display an error message if the input is invalid
                }

                switch (option) // perform action based on the user's input
                {
                    case 1:
                        AddRecipe(recipes); // call the AddRecipe method to enter a new recipe
                        clonedRecipes = SaveOriginalQuantities(recipes); // save the original ingredient quantities
                        break;
                    case 2:
                        ConvertUnits(recipes); // call the convert method to display the recipe
                        break;
                    case 3:
                        // ask the user to enter the scale factor and perform the scaling operation
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Select a recipe to Scale: \n type the name exactly");
                        Console.ForegroundColor = ConsoleColor.White;
                        int index = 1;
                        foreach (var recipe in recipes)
                        {
                            Console.WriteLine($"{index}. {recipe.Key}");
                            index++;
                        }

                        string selectedRecipeIndex = Console.ReadLine();
                        string recipeName = selectedRecipeIndex;

                        if (!recipes.ContainsKey(recipeName))
                        {
                            Console.WriteLine("Invalid recipe index.");
                            return;
                        }

                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(
                            "Enter scale factor: \n 1 : 0.5 (half) \n 2 : 2 (double) \n 3 : 3(triple) \n type only the number");
                        Console.ForegroundColor = ConsoleColor.White;
                        double scalingFactor = 0;
                        int scale;
                        while (!int.TryParse(Console.ReadLine(),
                                   out scale)) // read user input and check if it is a valid integer
                        {
                            ErrorMessage(); // display an error message if the input is invalid
                        }

                        switch (scale) // perform scaling based on the user's input
                        {
                            case 1:
                                scalingFactor = 0.5;
                                ScaleRecipe(recipes, recipeName,
                                    scalingFactor); // call the ScaleRecipe method to scale the recipe
                                ConvertUnits(recipes); // display the scaled recipe
                                Console.WriteLine("your recipe has been scaled!");
                                break;
                            case 2:
                                scalingFactor = 2;
                                ScaleRecipe(recipes, recipeName,
                                    scalingFactor); // call the ScaleRecipe method to scale the recipe
                                ConvertUnits(recipes); // display the scaled recipe
                                Console.WriteLine("your recipe has been scaled!");
                                break;
                            case 3:
                                scalingFactor = 3;
                                ScaleRecipe(recipes, recipeName,
                                    scalingFactor); // call the ScaleRecipe method to scale the recipe
                                ConvertUnits(recipes); // display the scaled recipe
                                Console.WriteLine("your recipe has been scaled!");
                                break;
                            default:
                                Console.WriteLine("Invalid option");
                                break;
                        }

                        //ViewRecipes(recipes);
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Resetting quantities...");
                        ResetQuantities(recipes,
                            clonedRecipes); // call the ResetQuantities method to reset the ingredient quantities
                        Console.WriteLine("Without conversions :");
                        Console.ForegroundColor = ConsoleColor.White;
                        ViewRecipes(recipes);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("With conversions :");
                        Console.ForegroundColor = ConsoleColor.White;
                        ConvertUnits(recipes);
                        break;

                    case 5:
                        ClearRecipe(recipes); // call the ClearRecipe method to clear the recipe
                        break;
                    case 6:
                        Console.WriteLine("Exiting..."); // exit the program
                        return;
                    default:
                        Console.WriteLine(
                            "Invalid option. Please try again."); // display an error message for invalid option
                        break;
                }
            }
        }

        public static void ErrorMessage()
        {
            // Set console foreground color to red
            Console.ForegroundColor = ConsoleColor.Red;
            // Print the error message
            Console.WriteLine(
                "Invalid input. Please enter a valid number. E.G. an integer.\n or type a number that matches an option presented.");
            // Set console foreground color back to white
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void AddRecipe(SortedDictionary<string, Recipe> recipes)
        {
            // Set console text color to blue
            Console.ForegroundColor = ConsoleColor.Blue;
            // Prompt user to enter recipe name
            Console.WriteLine("Enter recipe name:");
            // Set console text color to white
            Console.ForegroundColor = ConsoleColor.White;
            // Read recipe name from user input and assign to recipe Name variable
            string name = Console.ReadLine();

            if (recipes.ContainsKey(name))
            {
                Console.WriteLine("Recipe with the same name already exists.");
                return;
            }

            Recipe recipe = new Recipe(); // new recipe object to be added to SortedDictionary
            recipe.Name = name;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Enter number of ingredients:");
            Console.ForegroundColor = ConsoleColor.White;
            int ingredientCount = int.Parse(Console.ReadLine());

            // Loop through each ingredient and prompt user to enter name, quantity, and unit of measurement
            for (int i = 0; i < ingredientCount; i++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Enter ingredient name:");
                Console.ForegroundColor = ConsoleColor.White;
                string ingredientName = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Enter ingredient quantity:");
                Console.ForegroundColor = ConsoleColor.White;
                string quantity = Console.ReadLine();
                // Set console text color to white and prompt user to choose a unit of measurement option from a list
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Enter unit of measurement: \n 1. Teaspoons \n2. Tablespoons \n3. Cups ");
                Console.ForegroundColor = ConsoleColor.White;
                int option = Convert.ToInt32(Console.ReadLine());
                string unit = " ";
                // Assign unit of measurement based on option selected by user to ingredientUnits array
                switch (option)
                {
                    case 1:
                        unit = "Teaspoons";
                        break;
                    case 2:
                        unit = "Tablespoons";
                        break;
                    case 3:
                        unit = "Cups";
                        break;
                }

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Enter number of calories:");
                Console.ForegroundColor = ConsoleColor.White;
                int calories = int.Parse(Console.ReadLine());

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Enter food group:");
                Console.ForegroundColor = ConsoleColor.White;
                string foodGroup = Console.ReadLine();

                Ingredient ingredient = new Ingredient
                {
                    Name = ingredientName,
                    Quantity = quantity,
                    MeasurementUnit = unit,
                    Calories = calories,
                    FoodGroup = foodGroup
                };

                recipe.Ingredients.Add(ingredient);
            }

            // Set console text color to blue and prompt user to enter number of steps
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Enter number of steps:");
            Console.ForegroundColor = ConsoleColor.White;
            int stepCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < stepCount; i++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Enter step:");
                Console.ForegroundColor = ConsoleColor.White;
                string step = Console.ReadLine();
                recipe.Steps.Add(step);
            }

            recipes.Add(name, recipe);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Recipe added successfully.");
            Console.ForegroundColor = ConsoleColor.White;

            CheckCalories(recipe, OnRecipeCaloriesExceeded);
        }

        static void ViewRecipes(SortedDictionary<string, Recipe> recipes)
        {
            if (recipes.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No recipes found.");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Select a recipe to view:");
            Console.ForegroundColor = ConsoleColor.White;

            int index = 1;
            foreach (var recipe in recipes)
            {
                Console.WriteLine($"{index}. {recipe.Key}");
                index++;
            }

            int selectedRecipeIndex = int.Parse(Console.ReadLine());

            if (selectedRecipeIndex < 1 || selectedRecipeIndex > recipes.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid recipe index.");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            string selectedRecipeName = recipes.Keys.ElementAt(selectedRecipeIndex - 1);
            Recipe selectedRecipe = recipes[selectedRecipeName];

            Console.WriteLine($"Recipe: {selectedRecipe.Name}");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Ingredients:");
            Console.ForegroundColor = ConsoleColor.White;
            int totalCalories = 0;
            foreach (var ingredient in selectedRecipe.Ingredients)
            {
                Console.WriteLine(
                    $"{ingredient.Name} - {ingredient.Quantity} {ingredient.MeasurementUnit} {ingredient.Calories} {ingredient.FoodGroup}");
                totalCalories += ingredient.Calories;
            }

            Console.WriteLine($"Total Calories: {totalCalories}");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Steps:");
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var step in selectedRecipe.Steps)
            {
                Console.WriteLine(step);
            }
        }

        static void ScaleRecipe(SortedDictionary<string, Recipe> recipes, string recipeName, double scalingFactor)
        {
            if (recipes.ContainsKey(recipeName))
            {
                Recipe recipe = recipes[recipeName];
                foreach (var ingredient in recipe.Ingredients)
                {
                    double scaledQuantity = double.Parse(ingredient.Quantity) * scalingFactor;
                    ingredient.Quantity = scaledQuantity.ToString();
                }
            }
            else
            {
                Console.WriteLine("Recipe not found!");
            }
        }

        static SortedDictionary<string, Recipe> SaveOriginalQuantities(SortedDictionary<string, Recipe> recipes)
        {
            SortedDictionary<string, Recipe> clonedRecipes = new SortedDictionary<string, Recipe>();

            // Clone the recipes and their ingredients
            foreach (var recipe in recipes)
            {
                Recipe clonedRecipe = new Recipe
                {
                    Name = recipe.Value.Name,
                    Ingredients = new List<Ingredient>(),
                    Steps = new List<string>(recipe.Value.Steps)
                };

                foreach (var ingredient in recipe.Value.Ingredients)
                {
                    Ingredient clonedIngredient = new Ingredient
                    {
                        Name = ingredient.Name,
                        Quantity = ingredient.Quantity,
                        MeasurementUnit = ingredient.MeasurementUnit,
                        Calories = ingredient.Calories,
                        FoodGroup = ingredient.FoodGroup
                    };

                    clonedRecipe.Ingredients.Add(clonedIngredient);
                }

                clonedRecipes.Add(clonedRecipe.Name, clonedRecipe);
            }

            return clonedRecipes;
        }

        static void ResetQuantities(SortedDictionary<string, Recipe> recipes,
            SortedDictionary<string, Recipe> clonedRecipes)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Enter the name of the recipe to reset quantities:");
            Console.ForegroundColor = ConsoleColor.White;
            string recipeName = Console.ReadLine();

            if (recipes.ContainsKey(recipeName) && clonedRecipes.ContainsKey(recipeName))
            {
                Recipe originalRecipe = recipes[recipeName];
                Recipe clonedRecipe = clonedRecipes[recipeName];

                originalRecipe.Ingredients = new List<Ingredient>(clonedRecipe.Ingredients);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Quantities reset successfully.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Recipe not found or original quantities not saved.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        static void ClearRecipe(SortedDictionary<string, Recipe> recipes)
        {
            recipes.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Recipe dictionary cleared.");
            Console.ForegroundColor = ConsoleColor.White;
        }


        static void CheckCalories(Recipe recipe, RecipeCaloriesExceededHandler handler)
        {
            int totalCalories = 0;
            foreach (var ingredient in recipe.Ingredients)
            {
                totalCalories += ingredient.Calories;
            }

            if (totalCalories > 300)
            {
                handler(recipe.Name, totalCalories);
            }
        }

        static void OnRecipeCaloriesExceeded(string recipeName, int calories)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Warning: The recipe '{recipeName}' exceeds 300 calories. (Total Calories: {calories})");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void ConvertUnits(SortedDictionary<string, Recipe> recipes)
        {
            // The below variables are used to store the conversion amounts
            double teaspoonsToTablespoons = 1.0 / 3.0;
            double tablespoonsToCups = 1.0 / 16.0;
            double teaspoonsToCups = 1.0 / 48.0;

            foreach (var recipe in recipes)
            {
                Console.WriteLine($"Recipe: {recipe.Key}");

                foreach (var ingredient in recipe.Value.Ingredients)
                {
                    string ingredientName = ingredient.Name;
                    double ingredientQuantity = double.Parse(ingredient.Quantity);
                    string ingredientUnit = ingredient.MeasurementUnit;

                    // switch statement is used to convert different measurements
                    switch (ingredientUnit)
                    {
                        // 48 teaspoons makes a cup so it checks if teaspoons is more than a cup
                        case "Teaspoons" when ingredientQuantity >= 48:
                        {
                            // converts teaspoons directly into cups
                            int cups = (int)(ingredientQuantity * teaspoonsToCups);
                            // converts to teaspoons first then at the end converts to tablespoons
                            int tablespoonsRemaining =
                                (int)((ingredientQuantity - (cups * 48)) * teaspoonsToTablespoons);
                            // converts cups and tablespoonsRemaining to teaspoons and subtracts to get remainder teaspoons
                            int teaspoonsRemaining =
                                (int)(((ingredientQuantity - (cups * 48)) - (tablespoonsRemaining * 3)));
                            // Outputs all the units that have been converted and gives the remainder
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine(
                                $"{cups} Cups {tablespoonsRemaining} Tablespoons {teaspoonsRemaining} Teaspoons of {ingredientName} with calories of {ingredient.Calories} which belongs to the food group {ingredient.FoodGroup}");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                            break;
                        // 48 teaspoons makes a cup so it checks if teaspoons is less than a cup
                        case "Teaspoons" when ingredientQuantity < 48:
                        {
                            // converts teaspoons directly into tablespoons
                            int tablespoons = (int)(ingredientQuantity * teaspoonsToTablespoons);
                            // converts tablespoons to teaspoons and subtracts the two to get remainder of teaspoons
                            int teaspoonsRemaining = (int)(ingredientQuantity - (tablespoons * 3));
                            // Outputs to the console with the converted measurements
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine(
                                $"{tablespoons} Tablespoons {teaspoonsRemaining} Teaspoons of {ingredientName} with calories of {ingredient.Calories} which belongs to the food group {ingredient.FoodGroup}");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                            break;
                        // 16 tablespoons are in a cup so a check is made to see if there are enough tablespoons to make a cup
                        case "Tablespoons" when ingredientQuantity >= 16:
                        {
                            int cups = (int)(ingredientQuantity * tablespoonsToCups);
                            int tablespoonsRemaining = (int)((ingredientQuantity - (cups * 16)));
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine(
                                $"{cups} Cups {tablespoonsRemaining} Tablespoons of {ingredientName} with calories of {ingredient.Calories} which belongs to the food group {ingredient.FoodGroup}");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                            break;
                        // 16 tablespoons are in a cup so a check is made to see if there are not enough tablespoons to make a cup
                        case "Tablespoons" when ingredientQuantity < 16:
                        {
                            int tablespoonsRemaining = (int)(ingredientQuantity);
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine(
                                $"{tablespoonsRemaining} Tablespoons of {ingredientName} with calories of {ingredient.Calories} which belongs to the food group {ingredient.FoodGroup}");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                            break;
                        case "Cups": // No conversion is needed as cups is the highest measurement available
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine(
                                $"{ingredientQuantity} Cups of {ingredientName} with calories of {ingredient.Calories} which belongs to the food group {ingredient.FoodGroup}");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        default: // Handle invalid units here
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine(
                                $"Invalid unit of measurement for ingredient {ingredientName}: {ingredientUnit} with calories of {ingredient.Calories} which belongs to the food group {ingredient.FoodGroup}");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }
                }
            }
        }
    }

    class Recipe
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<string> Steps { get; set; }

        public Recipe()
        {
            Ingredients = new List<Ingredient>();
            Steps = new List<string>();
        }
    }

    class Ingredient
    {
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string MeasurementUnit { get; set; }
        public int Calories { get; set; }
        public string FoodGroup { get; set; }
    }
}