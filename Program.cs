using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp
{
    public class Program
    {
        //List<T> of Recipe Class
        private static List<Recipe> recipes = new List<Recipe>();

        //delegates
        private delegate void ExceedTotalCaloriesDelegate(Recipe recipe);

        #region Main
        static void Main(string[] args)
        {

            //Main Menu
            mainMenu();

        }
        #endregion

        #region MainMenu
        public static void mainMenu()
        {

            Console.WriteLine("******************************\n"
                             + "Welcome to Recipe Application\n"
                             + "******************************");
            bool exit = true;
            do
            {
                Console.WriteLine("\nWelcome to the Main Menu of RecipeApp v2.0.\nEnter a number to continue:");
                Console.WriteLine("1. Add Recipe?" +
                    "\n2. Display All Recipes" +
                    "\n3. Display Recipe By Name" +
                    "\n4. Scale Recipe" +
                    "\n5. Reset Quantities" +
                    "\n6. Clear Recipe" +
                    "\n7. Exit");
                Console.Write("Enter the number:\n> ");
                string user = Console.ReadLine();
                //user selection
                switch (user)
                {
                    case "1":
                        AddRecipe();
                        break;
                    case "2":
                        DisplayRecipes();
                        break;
                    case "3":
                        PrintRecipeByName();
                        break;
                    case "4":
                        ScaleRecipe();
                        break;
                    case "5":
                        ResetQuantities();
                        break;
                    case "6":
                        ClearRecipe();
                        break;
                    case "7":
                        Console.WriteLine("Exiting...");
                        exit = false;
                        break;
                    default:
                        Console.WriteLine("Enter a valid number... Try Again\n");
                        break;
                }
            } while (exit);
        }
        #endregion

        #region AddRecipe
        public static void AddRecipe()
        {
            //Recipe declaration
            Recipe recipe = new Recipe();

            //control variable
            bool flag = true;

            //catching any exceptions like wrong user input and repeating until its right
            while (flag)
            {
                try
                {
                    //name of the Recipe
                    Console.Write("Enter the name of the recipe:\n> ");
                    recipe.recipeName = Console.ReadLine().ToLower();

                    //asking user for number of ingredients
                    Console.Write("Enter the number of ingredients:\n> ");
                    int numIngredients = int.Parse(Console.ReadLine());

                    //asking user for name, quantity & unit measurement of ingredient
                    for (int i = 0; i < numIngredients; i++)
                    {
                        Console.WriteLine($"Ingredient: {i + 1}");

                        //ingredient name
                        Console.Write("Enter the name of ingredient {0}:\n> ", i + 1);
                        string name = Console.ReadLine();

                        //ingredient quantity
                        Console.Write("Enter the quantity of {0}:\n> ", name);
                        double quantity = double.Parse(Console.ReadLine().Replace(".", ","));

                        //ingredient unit measurement
                        Console.Write("Enter the unit of measurement of {0}:\n" +
                            "Examples:tsp, tbsp, Cup(s), g, kg" +
                            "\n> ", name);
                        string unit = Console.ReadLine();

                        //ingredient calories
                        Console.Write("Enter the calories of {0}:\n> ", name);
                        double calories = int.Parse(Console.ReadLine());

                        //ingredient foodGroup
                        Console.Write("Enter the Food Group of {0}:\n> ", name);
                        string foodGroup = Console.ReadLine();

                        //passing the variables to addIngredients method in Recipe class
                        recipe.addIngredient(name, quantity, unit, calories, foodGroup);
                    }

                    //asking user for number of steps to make said recipe
                    Console.Write("Enter the number of steps required:\n> ");
                    int numSteps = int.Parse(Console.ReadLine());

                    //forloop for control how many steps there are
                    Console.WriteLine("Enter instructions for Steps: ");
                    for (int s = 0; s < numSteps; s++)
                    {
                        Console.Write("{0} - ", s + 1);
                        recipe.addStep(Console.ReadLine());

                    }

                    //adding into recipe List<T>
                    recipes.Add(recipe);

                    //confirmation that recipe has been added
                    Console.WriteLine($"Recipe '{recipe.recipeName}' has been successfully added!");

                    //control variable
                    flag = false;

                }
                catch (Exception)
                {
                    Console.WriteLine("Wrong input... try again");

                    //control variable
                    flag = true;
                }
            }
        }
        #endregion

        #region FindRecipe
        static Recipe FindRecipe()
        {
            Recipe recipe;
            if (recipes.Count > 0)
            {
                //printing recipe names
                DisplayRecipes();

                //asking user for recipe input
                Console.Write("\nPlease enter a recipe name:\n> ");
                string userInput = Console.ReadLine().ToLower();

                //finding recipe via recipeName while matching the userInput
                recipe = recipes.FirstOrDefault(r => r.recipeName == userInput);

                return recipe;

            }
            else
            {
                Console.WriteLine("\n********************************");
                Console.WriteLine("No Recipe found! Please Enter a Recipe...");
                Console.WriteLine("********************************\n");

                return null;
            }

        }
        #endregion

        #region ScaleRecipe
        //Scale factor method to scale the ingredient amounts
        public static void ScaleRecipe()
        {
            //checking recipe count
            if (recipes.Count > 0)
            {
                //for invlid input
                while (true)
                {
                    //finding the recipe
                    Recipe recipe = FindRecipe();

                    //checking if recipe is null
                    if (recipe == null)
                    {
                        Console.WriteLine("Invalid Input! Please Try Again...");
                    }
                    else
                    {

                        Console.WriteLine("\n********************************");
                        Console.WriteLine("Scaling Recipe...");
                        Console.WriteLine("********************************");
                        Console.WriteLine("Please enter 0.5, 2, or 3 to scale your recipe: ");
                        Console.WriteLine("or 1 to cancel");
                        Console.Write("> ");
                        double factor = double.Parse(Console.ReadLine().Replace(".", ","));

                        //Only allowing user to enter 0.5, 2, or 3, as per the POE.
                        //1 for cancel (default)
                        if (factor == 0.5 || factor == 1 || factor == 2 || factor == 3)
                        {
                            foreach (var ingredient in recipe.Ingredients)
                            {
                                ingredient.Quantity *= factor;
                                ingredient.Calories *= factor;
                                ingredient.ScaleFactor = factor;
                            }
                            Console.WriteLine("\nSuccessfully scaled the recipe!");

                            //breaking the loop if its successful
                            return;
                        }
                        else
                        {
                            Console.WriteLine("\nYou entered the wrong scale factor number...");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("\n********************************");
                Console.WriteLine("No Recipe found! Please Enter a Recipe...");
                Console.WriteLine("********************************\n");
            }


        }
        #endregion

        #region ResetQuantities
        //Method that resets the quantities back to default,
        //and resets the scale factor back to 1 (default)
        public static void ResetQuantities()
        {
            if (recipes.Count > 0)
            {
                while (true)
                {
                    //finding the recipe
                    Recipe recipe = FindRecipe();

                    //checking if recipe is null
                    if (recipe == null)
                    {
                        Console.WriteLine("Invalid Input! Please Try Again...");

                    }
                    else
                    {


                        Console.WriteLine("\n********************************");
                        Console.WriteLine("Resetting Quantities...");
                        Console.WriteLine("********************************");

                        //resetting every quantity and scalefactor back to normal
                        foreach (var ingredient in recipe.Ingredients)
                        {
                            ingredient.Quantity /= ingredient.ScaleFactor;
                            ingredient.Calories /= ingredient.ScaleFactor;
                            ingredient.ScaleFactor = 1;
                        }
                        Console.WriteLine("\nSuccessfully Resetted to Previous Quantity!");

                        //breaking loop
                        return;
                    }
                }
            }
            else
            {
                Console.WriteLine("\n********************************");
                Console.WriteLine("No Recipe found! Please Enter a Recipe...");
                Console.WriteLine("********************************\n");
            }
        }
        #endregion

        #region ClearRecipe
        //Method that clears all the stored recipes
        public static void ClearRecipe()
        {
            //checking recipe count
            if (recipes.Count > 0)
            {
                //for invlid input
                while (true)
                {
                    //finding the recipe
                    Recipe recipe = FindRecipe();

                    //checking if recipe is null
                    if (recipe == null)
                    {
                        Console.WriteLine("Invalid Input! Please Try Again...");

                    }
                    else
                    {
                        //for user confirmation
                        while (true)
                        {
                            Console.Write($"Are you sure you want to delete recipe '{recipe.recipeName}'? (type 'yes' or 'no')\n> ");
                            string input = Console.ReadLine().ToLower();

                            //checking user input

                            if (input.Equals("yes"))
                            {

                                Console.WriteLine("\n********************************");
                                Console.WriteLine("Clearing Recipe...");
                                Console.WriteLine("********************************");

                                //setting a temp variable so it can print the name of the recipe after its deleted
                                string name = recipe.recipeName;

                                //using .remove() to remove a specific recipe found earlier
                                recipes.Remove(recipe);

                                //telling user it cleared the recipe
                                Console.WriteLine($"\nSuccessfully deleted recipe '{name}'!");

                                //breaking loop
                                return;

                            }
                            else if (input.Equals("no"))
                            {
                                Console.WriteLine("\nReturning to Main Menu...");

                                //breaking loop
                                return;
                            }
                            else
                            {
                                Console.WriteLine("Please Type 'yes' Or 'no'");
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("\n********************************");
                Console.WriteLine("No Recipe found! Please Enter a Recipe...");
                Console.WriteLine("********************************\n");
            }

        }
        #endregion

        #region DisplayRecipes
        //Method that prints the recipe names out
        public static void DisplayRecipes()
        {
            if (recipes.Count > 0)
            {
                Console.WriteLine("\n********************************");
                Console.WriteLine("Printing Recipe Names");
                Console.WriteLine("**********************************");

                //finding matching recipe and ordering it alphabetically
                List<string> recipeNames = recipes.Select(r => r.recipeName).OrderBy(name => name).ToList();

                foreach (var rname in recipeNames)
                {
                    Console.WriteLine(rname);
                }

            }
            else
            {
                Console.WriteLine("\n********************************");
                Console.WriteLine("No Recipe found! Please Enter a Recipe...");
                Console.WriteLine("********************************\n");
            }

        }
        #endregion

        #region PrintRecipeByName
        //method that prints a recipe by a name
        public static void PrintRecipeByName()
        {
            if (recipes.Count > 0)
            {
                while (true)
                {
                    Recipe recipe = FindRecipe();

                    //checking if recipe is null
                    if (recipe == null)
                    {
                        Console.WriteLine("Invalid Input! Please Try Again...");
                    }
                    else
                    {
                        //printing recipe with the findRecipe method
                        PrintRecipe(recipe);

                        //breaking loop
                        return;
                    }
                }
            }
            else
            {
                Console.WriteLine("\n********************************");
                Console.WriteLine("No Recipe found! Please Enter a Recipe...");
                Console.WriteLine("********************************\n");
            }

        }
        #endregion

        #region PrintRecipe
        static void PrintRecipe(Recipe recipe)
        {
            //printing recipe name
            Console.WriteLine("Recipe Details:");
            Console.WriteLine($"Name: {recipe.recipeName}");

            //displaying recipe ingredients
            Console.WriteLine("Ingredients:");
            foreach (var ingredient in recipe.Ingredients)
            {
                Console.WriteLine($"- {ingredient.Quantity} {ingredient.Unit} of {ingredient.Name}\n");
                Console.WriteLine($"Calories: {ingredient.Calories}");
                Console.WriteLine($"Food Group: {ingredient.FoodGroup}\n");
            }

            //displaying recipe steps
            Console.WriteLine("Steps:");
            foreach (var step in recipe.Steps)
            {
                Console.WriteLine($"-{step.Steps}");
            }

            //displaying total calories with totalCalories(recipe)
            Console.WriteLine($"\nTotal Calories: {TotalCalories(recipe)}");

            //creating object of the delegate to use to display the totalCalories method
            ExceedTotalCaloriesDelegate etcd = exceedTotalCalories;

            //printing exceedTotalCalories method via delegate
            etcd(recipe);

        }
        #endregion

        #region TotalCalories
        public static double TotalCalories(Recipe recipe)
        {
            //calculating calories from the found recipe from printRecipe method
            double totalCalories = recipe.Ingredients.Sum(ingredient => ingredient.Calories);

            //returning the value so it can be used for exceedTotalCalories
            return totalCalories;
        }
        #endregion

        #region ExceedTotalCalories
        public static void exceedTotalCalories(Recipe recipe)
        {
            if (TotalCalories(recipe) > 300)
            {
                //printing warning if it exceeds 300 calories
                Console.WriteLine($"WARNING: Total calories for {recipe.recipeName} exceed 300!");

            }
        }
        #endregion
    }
}
