using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipeApp;
using System;

namespace RecipeApp2Tests
{
    [TestClass]
    public class ProgramTest
    {
        [TestMethod()]
        public void TotalCaloriesTest()
        {
            // Arrange
            Recipe recipe = new Recipe();
            recipe.addIngredient("", 0, "", 50, "");
            recipe.addIngredient("", 0, "", 100, "");
            recipe.addIngredient("", 0, "", 75, "");
            double actual = 225;

            // Act
            double totalCalories = Program.TotalCalories(recipe);

            // Assert
            Assert.AreEqual(actual, totalCalories);
        }
    }
}
