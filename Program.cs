using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp
{
    class Program
    {
        //object of Recipe
        public static Recipe Recipe = new Recipe();
        #region Main
        static void Main(string[] args)
        {
            //Main Menu
            mainMenu();

            //keep console open
            Console.ReadLine();

        }
        #endregion

        #region MainMenu
        public static void mainMenu()
        {
            Console.WriteLine("******************************");
            Console.WriteLine("Welcome to Recipe Application");
            Console.WriteLine("******************************");

            while (true)
            {
                Console.WriteLine("\nWelcome to the Main Menu of RecipeApp.\nEnter a number to continue:");
                Console.WriteLine("1. Add Recipe?" +
                    "\n2. Display Recipe" +
                    "\n3. Scale Recipe" +
                    "\n4. Reset Quantities" +
                    "\n5. Clear Recipe" +
                    "\n6. Exit");
                Console.Write("Enter the number:\n> ");
                int user = int.Parse(Console.ReadLine());
                //user selection
                switch (user)
                {
                    case 1:
                        addRecipe();
                        break;
                    case 2:
                        Recipe.printRecipe();
                        break;
                    case 3:
                        Recipe.scaleRecipe();
                        break;
                    case 4:
                        Recipe.resetQuantities();
                        break;
                    case 5:
                        Recipe.clearRecipe();
                        break;
                    case 6:
                        Console.WriteLine("Exiting...");
                        System.Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Enter a valid number... Try Again\n");
                        break;
                }
            }
        }
        #endregion

        #region addRecipe
        public static void addRecipe()
        {
            try
            {
                //name of the Recipe
                Console.Write("Enter the name of the recipe:\n> ");
                string recipeName = Console.ReadLine().ToLower();
                Recipe.recipeName = recipeName;

                //asking user for number of ingredients
                Console.Write("Enter the number of ingredients:\n> ");
                int numIngredients = int.Parse(Console.ReadLine());

                //asking user for name, quantity & unit measurement of ingredient
                for (int i = 0; i < numIngredients; i++)
                {
                    Console.Write("Enter the name of ingredient {0}:\n> ", i + 1);
                    string name = Console.ReadLine();

                    Console.Write("Enter the quantity of {0}:\n> ", name);
                    double quantity = int.Parse(Console.ReadLine());

                    Console.Write("Enter the unit of measurement of {0}:\n> ", name);
                    string unit = Console.ReadLine();

                    //passing the variables to addIngredients method in Recipe class
                    Recipe.addIngredient(name, quantity, unit);
                }

                //asking user for steps to make the recipe
                Console.Write("Enter the number of steps required:\n> ");
                int numberSteps = int.Parse(Console.ReadLine());

                //forloop for control how many steps there are

                Console.WriteLine("Enter instructions for Steps: ");
                for (int s = 0; s < numberSteps; s++)
                {
                    Console.Write("{0}- ", s + 1);
                    Recipe.addStep(Console.ReadLine());

                }
            }
            catch (Exception)
            {
                Console.WriteLine("Wrong input... try again");
            }

            


        }
        #endregion
    }
}
