using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RecipeApp3
{
    public class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public double ScaleFactor { get; set; }
        public double Calories { get; set; }
        public string FoodGroup { get; set; }
        public TextBox IngredientNameTextBox { get; set; }
        public TextBox QuantityTextBox { get; set; }
        public ComboBox UnitTextBox { get; set; }
        public TextBox CaloriesTextBox { get; set; }
        public ComboBox FoodGroupTextBox { get; set; }

        public Ingredient(TextBox ingredientNameTextBox, TextBox quantityTextBox, ComboBox unitTextBox, TextBox caloriesTextBox, ComboBox foodGroupTextBox)
        {
            IngredientNameTextBox = ingredientNameTextBox;
            QuantityTextBox = quantityTextBox;
            UnitTextBox = unitTextBox;
            CaloriesTextBox = caloriesTextBox;
            FoodGroupTextBox = foodGroupTextBox;
        }

        public Ingredient(string name, double quantity, string unit, double calories, string foodGroup)
        {
            Name = name;
            Quantity = quantity;
            Unit = unit;
            ScaleFactor = 1;
            Calories = calories;
            FoodGroup = foodGroup;
        }
    }
}
