using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RecipeApp3
{
    public class Recipe
    {
        #region Declarations
        //Declaration of Lists
        public List<Ingredient> Ingredients { get; set; }
        public List<Step> Steps { get; set; }
        public string recipeName { get; set; }
        #endregion

        #region Recipe Constructor
        //Constructor creating the List Collection
        public Recipe()
        { 
            Ingredients = new List<Ingredient>();
            Steps = new List<Step>();
        }
        #endregion

        #region addIngredient
        //Method to add ingredients to a recipe
        public void addIngredient(string name, double quantity, string unit, double calories, string foodGroup)
        {
            //creating new instance of Ingredients & passing them
            Ingredients.Add(new Ingredient(name, quantity, unit, calories, foodGroup));
        }
        #endregion

        #region addStep
        //Method to add steps to a recipe
        public void addStep(string steps)
        {
            Steps.Add(new Step(steps));
        }
        #endregion
    }

}

