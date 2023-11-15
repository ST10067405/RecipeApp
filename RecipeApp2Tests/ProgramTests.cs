using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipeApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void totalCaloriesTest()
        {
            // Arrange
            Recipe recipe = new Recipe();
            recipe.addIngredient("", 0, "", 50, "");
            recipe.addIngredient("", 0, "", 100, "");
            recipe.addIngredient("", 0, "", 75, "");
            double expected = 225;

            // Act
            double totalCalories = Program.totalCalories(recipe);

            // Assert
            Assert.AreEqual(expected, totalCalories);
        }
    }
}