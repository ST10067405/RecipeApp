using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RecipeApp
{
    class Recipe
    {
        #region Declarations
        //Declaration of Lists
        public List<Ingredient> Ingredients { get; set; }
        public List<string> Steps { get; set; }
        public string recipeName { get; set; }
        #endregion

        #region Recipe Constructor
        //Constructor creating the List Collection
        public Recipe()
        {
            Ingredients = new List<Ingredient>();
            Steps = new List<string>();
        }
        #endregion

        #region addIngredient
        //Method to add ingredients to a recipe
        public void addIngredient(string name, double quantity, string unit)
        {
            //creating new instance of Ingredients & passing them
            Ingredients.Add(new Ingredient(name, quantity, unit));
        }
        #endregion

        #region addStep
        //Method to add steps to a recipe
        public void addStep(string steps)
        {
            Steps.Add(steps);
        }
        #endregion

        #region scaleRecipe
        //Scale factor method to scale the ingredient amounts
        public void scaleRecipe()
        {
            //to check there is a recipe to scale
            if (Ingredients.Count > 0)
            {
                //control variable called flag
                bool flag = true;
                do
                {
                    Console.WriteLine("\n********************************");
                    Console.WriteLine("Scaling Recipe...");
                    Console.WriteLine("********************************");
                    Console.WriteLine("Please enter 0.5, 2, or 3 to scale your recipe: ");
                    Console.WriteLine("or 1 to cancel");
                    Console.Write("> ");
                    double factor = double.Parse(Console.ReadLine());

                    //tryctach to catch exception
                    try
                    {
                        //Only allowing user to enter 0.5, 2, or 3, as per the POE.
                        //1 for cancel (default)
                        if (factor == 0.5 || factor == 1 || factor == 2 || factor == 3)
                        {
                            foreach (Ingredient ingredient in Ingredients)
                            {
                                ingredient.Quantity *= factor;
                                ingredient.ScaleFactor = factor;
                            }
                            flag = false;
                        }
                        else
                        {
                            Console.WriteLine("You entered the wrong scale factor number...");
                        }

                    }
                    catch (Exception) 
                    { 
                        Console.WriteLine("You entered the wrong scale factor number..."); 
                    }
                } while (flag == true);
                Console.WriteLine("********************************\n");
            }
            else
            {
                Console.WriteLine("********************************");
                Console.WriteLine("No Recipe to Scale! Please Enter a Recipe...");
                Console.WriteLine("********************************\n");
            }


        }
        #endregion

        #region resetQuantities
        //Method that resets the quantities back to default,
        //and resets the scale factor back to 1 (default)
        public void resetQuantities()
        {

            //to check there is a recipe to reset
            if (Ingredients.Count > 0)
            {
                Console.WriteLine("\n********************************");
                Console.WriteLine("Resetting Quantities...");
                Console.WriteLine("********************************");

                //resetting every quantity and scalefactor back to normal
                foreach (Ingredient ingredient in Ingredients)
                {
                    ingredient.Quantity /= ingredient.ScaleFactor;
                    ingredient.ScaleFactor = 1;
                }
                Console.WriteLine("********************************\n");
            }
            else
            {
                Console.WriteLine("********************************");
                Console.WriteLine("No Recipe to Reset! Please Enter a Recipe...");
                Console.WriteLine("********************************\n");
            }


        }
        #endregion

        #region clearRecipe
        //Method that clears all the stored recipes
        public void clearRecipe()
        {
            //to check if there is a recipe to scale
            if (Ingredients.Count > 0)
            {
                Console.WriteLine("\n********************************");
                Console.WriteLine("Clearing Recipe...");
                Console.WriteLine("********************************");
                //using .clear() to remove elements in the Lists 
                Ingredients.Clear();
                Steps.Clear();
                recipeName = null;
            }
            else
            {
                Console.WriteLine("********************************");
                Console.WriteLine("No Recipe to Clear! Please Enter a Recipe...");
                Console.WriteLine("********************************\n");
            }
        }
        #endregion

        #region printRecipe
        //Method that prints the recipe out
        public void printRecipe()
        {
            try
            {
                Console.WriteLine("\n********************************");
                Console.WriteLine("Printing Recipe: {0}", recipeName.ToUpper());
                Console.WriteLine("**********************************");
                Console.WriteLine("Ingredients:");
                foreach (Ingredient ingredient in Ingredients)
                {
                    Console.WriteLine("- {0} {1} {2}", ingredient.Quantity, ingredient.Unit, ingredient.Name);
                }

                Console.WriteLine("\nSteps:");
                for (int i = 0; i < Steps.Count; i++)
                {
                    Console.WriteLine("{0}) {1}", i + 1, Steps[i]);
                }
                Console.WriteLine("********************************");
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("No Recipe found! Please Enter a Recipe...");
                Console.WriteLine("********************************\n");
            }

        }
        #endregion

    }
}
