using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.DataVisualization.Charting;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using Microsoft.VisualBasic;

namespace RecipeApp3
{
    /// <summary>
    /// Interaction logic for AddRecipeWindow.xaml
    /// </summary>
    public partial class RecipeWindow : Window
    {
        #region Declarations
        //delegates
        private delegate void ExceedTotalCaloriesDelegate(Recipe recipe);

        //Lists
        private List<Recipe> recipes = new List<Recipe>();
        private List<Ingredient> ingredientTextBoxes = new List<Ingredient>();
        private List<Step> stepTextBoxes = new List<Step>();
        private List<StackPanel> stackPanels = new List<StackPanel>();

        //For FilterBy region
        // Property to store the selected food group
        public string SelectedFoodGroup { get; private set; }
        #endregion

        #region Constructor
        public RecipeWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region NAV
        private void Home(object sender, RoutedEventArgs e)
        {
            //close current screen and go back to home
            MainWindow mw = new MainWindow();
            mw.Show();

            this.Close();
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region AddRecipe
        //method to add ingredients 
        private void AddIngredientsFormat()
        {
            //create a new stackpanel
            StackPanel ingredPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 0, 0, 10)
            };
            //set the new textboxes that will get generated
            TextBox ingredientNameTextBox = new TextBox()
            {
                Width = 100,
                Margin = new Thickness(0, 0, 10, 0)
            };
            TextBox quantityTextBox = new TextBox()
            {
                Width = 30,
                Margin = new Thickness(0, 0, 10, 0)
            };
            ComboBox unitTextBox = new ComboBox()
            {
                Width = 100,
                Margin = new Thickness(0, 0, 10, 0)
            };
            TextBox caloriesTextBox = new TextBox()
            {
                Width = 30,
                Margin = new Thickness(0, 0, 10, 0)
            };
            ComboBox foodGroupTextBox = new ComboBox()
            {
                Width = 100,
                Margin = new Thickness(0, 0, 10, 0)
            };
            //adding unit items
            unitTextBox.Items.Add("ml");
            unitTextBox.Items.Add("L");
            unitTextBox.Items.Add("g");
            unitTextBox.Items.Add("kg");
            unitTextBox.Items.Add("Cup(s)");
            unitTextBox.Items.Add("Bowl(s)");
            unitTextBox.Items.Add("Teaspoon(s)");
            unitTextBox.Items.Add("Tablespoon(s)");

            //adding foodgroup items
            foodGroupTextBox.Items.Add("Starch");
            foodGroupTextBox.Items.Add("Fruits and Vegetables");
            foodGroupTextBox.Items.Add("Legumes");
            foodGroupTextBox.Items.Add("Proteins");
            foodGroupTextBox.Items.Add("Dairy");
            foodGroupTextBox.Items.Add("Fats & Oil");
            foodGroupTextBox.Items.Add("Water");

            // using .add method to add the textblock and the textboxes to ingredPanel
            ingredPanel.Children.Add(new TextBlock()
            {
                Text = "Name:",
                VerticalAlignment = VerticalAlignment.Center
            });
            ingredPanel.Children.Add(ingredientNameTextBox);
            ingredPanel.Children.Add(new TextBlock()
            {
                Text = "Quantity:",
                VerticalAlignment = VerticalAlignment.Center
            });
            ingredPanel.Children.Add(quantityTextBox);
            ingredPanel.Children.Add(new TextBlock()
            {
                Text = "Units:",
                VerticalAlignment = VerticalAlignment.Center
            });
            ingredPanel.Children.Add(unitTextBox);
            ingredPanel.Children.Add(new TextBlock()
            {
                Text = "Calories:",
                VerticalAlignment = VerticalAlignment.Center
            });
            ingredPanel.Children.Add(caloriesTextBox);
            ingredPanel.Children.Add(new TextBlock()
            {
                Text = "Food Group:",
                VerticalAlignment = VerticalAlignment.Center
            });
            ingredPanel.Children.Add(foodGroupTextBox);

            ingredientTextBoxes.Add(new Ingredient(ingredientNameTextBox, quantityTextBox, unitTextBox, caloriesTextBox, foodGroupTextBox));
            stackPanels.Add(ingredPanel);
            //displaying the inputBoxs for the user input values
            recipeDetails.Children.Add(ingredPanel);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //hiding the RichTextBox as it is not needed
            recipeRichTextBox.Visibility = Visibility.Collapsed;

            //ingredient number button
            int ingredCount;
            if (int.TryParse(IngredientCountTb.Text, out ingredCount))
            {
                for (int i = 0; i < ingredCount; i++)
                {
                    AddIngredientsFormat();
                }
                //reset add ingredients textbox
                IngredientCountTb.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("Input a valid ingredient amount! Must be a Whole Number");
                //reset add ingredients textbox
                IngredientCountTb.Text = string.Empty;
            }
        }

        private void AddStepFormat()
        {
            //create a new stackpanel
            StackPanel stepPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 0, 0, 10)
            };
            //setting new textboxes that will get generated
            TextBox stepTextBox = new TextBox()
            {
                Width = 150,
                Margin = new
                Thickness(0, 0, 10, 0)
            };

            stepPanel.Children.Add(new TextBlock()
            {
                Text = "Step:",
                VerticalAlignment = VerticalAlignment.Center
            });
            stepPanel.Children.Add(stepTextBox);

            stepTextBoxes.Add(new Step(stepTextBox));
            stackPanels.Add(stepPanel);
            //displaying the inputBoxs for the user input values
            recipeDetails.Children.Add(stepPanel);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Hide the RichTextBox as it is not needed
            recipeRichTextBox.Visibility = Visibility.Collapsed;

            //Add Steps button 
            int stepCount;
            if (int.TryParse(stepCountTb.Text, out stepCount))
            {
                for (int i = 0; i < stepCount; i++)
                {
                    // add step count method and pull
                    AddStepFormat();
                }
                //reset add steps textbox
                stepCountTb.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("Input a valid steps amount! Must be a Whole Number");
                //reset add ingredients textbox
                IngredientCountTb.Text = string.Empty;
            }
        }

        private void AddRecipeDetails(object sender, RoutedEventArgs e)
        {
            //creating object of recipe class
            Recipe recipe = new Recipe();

            //if theres no recipe name it will cancel
            if (RecipeNameTb.Text != "")
            {
                //taking the user input from the textboxes and adding it to the string/double variables used from part 2
                recipe.recipeName = RecipeNameTb.Text;

                try
                {
                    foreach (Ingredient itb in ingredientTextBoxes)
                    {
                        string ingredientName = itb.IngredientNameTextBox.Text;
                        double quantity = double.Parse(itb.QuantityTextBox.Text);
                        string unit = itb.UnitTextBox.Text;
                        double calories = double.Parse(itb.CaloriesTextBox.Text);
                        string foodGroup = itb.FoodGroupTextBox.Text;

                        recipe.addIngredient(ingredientName, quantity, unit, calories, foodGroup);
                    }

                    foreach (Step stb in stepTextBoxes)
                    {
                        string stepText = stb.StepsTextBox.Text;
                        recipe.addStep(stepText);
                    }

                    //adding to recipe list<T>
                    recipes.Add(recipe);

                    //adding to combolist list<T>
                    RecipeListCombo.Items.Add(recipe.recipeName);

                    //clearing recipename textbox
                    RecipeNameTb.Clear();

                    //IMPORTANT - clearing the textbox lists so the next recipe doesnt take the previous recipe's ingredients
                    ingredientTextBoxes.Clear();
                    stepTextBoxes.Clear();

                    //removing other textboxes
                    foreach (StackPanel sp in stackPanels)
                    {
                        sp.Children.Clear();
                    }

                    //messagebox to show that the recipe was successfully added
                    MessageBox.Show("Recipe Successfully Added");
                }
                catch (Exception)
                {

                    MessageBox.Show("Invalid Entry - Try Again!");
                }
            }
            else
            {
                MessageBox.Show("Recipe name cannot be empty!");
            }

        }

        #endregion

        #region Display
        private void Display(Recipe recipe)
        {
            //display recipe heading
            string headingRecipe = "Recipe Details: \n" +
                $"Name: {recipe.recipeName}";
            recipeRichTextBox.AppendText(headingRecipe);

            recipeRichTextBox.AppendText(Environment.NewLine);

            // ingredient list first 
            string headingIngred = "Ingredients: \n";
            recipeRichTextBox.AppendText(headingIngred);
            foreach (var ingredient in recipe.Ingredients)
            {

                string ingredientText = $"- {ingredient.Quantity} {ingredient.Unit} of {ingredient.Name}\n" +
                    $"Calories: {ingredient.Calories}\n" +
                    $"Food Group: {ingredient.FoodGroup}";
                recipeRichTextBox.AppendText(ingredientText);

                recipeRichTextBox.AppendText(Environment.NewLine);
            }

            // handle the steps info
            string headingSteps = "Steps: \n";
            recipeRichTextBox.AppendText(headingSteps);
            foreach (var step in recipe.Steps)
            {
                string stepText = $"-{step.Steps}";
                recipeRichTextBox.AppendText(stepText);
            }

            //total calories
            recipeRichTextBox.AppendText($"\nTotal Calories: {TotalCalories(recipe)}\n");

            //exceed total Calories
            //creating object of the delegate to use to display the exceedTotalCalories method
            ExceedTotalCaloriesDelegate etcd = exceedTotalCalories;

            //printing exceedTotalCalories method via delegate
            //includes 'recipeRichTextBox.AppendText'
            etcd(recipe);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (recipes.Count > 0)
            {


                //richtextbox visibility to show
                recipeRichTextBox.Visibility = Visibility.Visible;
                //clear the richtextbox first
                recipeRichTextBox.Document.Blocks.Clear();

                Recipe recipe = recipes.FirstOrDefault(r => r.recipeName == RecipeListCombo.SelectedItem as string);

                if (recipe != null)
                {
                    //display method here
                    Display(recipe);
                }
                else
                {
                    //messagebox to show that the recipe was not found
                    MessageBox.Show("No Recipe Found!");
                }
            }
            else
            {
                MessageBox.Show("Please enter a recipe!");
            }
        }
        #endregion

        #region ClearData
        private void ClearData(Recipe recipe)
        {
            //setting a temp variable so it can print the name of the recipe after its deleted
            string name = recipe.recipeName;

            //using .remove() to remove a specific recipe found earlier
            recipes.Remove(recipe);
            RecipeListCombo.Items.Remove(name);
            recipeRichTextBox.Document.Blocks.Clear();
            recipeRichTextBox.Visibility = Visibility.Collapsed;

            //messagebox to show that the recipe was deleted successfully
            MessageBox.Show($"\nSuccessfully deleted recipe '{name}'!");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //clear recipe button
            //checking for recipes to work with first
            if (recipes.Count > 0)
            {
                //taking user input using RecipeListCombo.SelectedItem and searching for the recipe.
                string userInput = RecipeListCombo.SelectedItem.ToString();
                Recipe recipe = recipes.FirstOrDefault(r => r.recipeName.Equals(userInput, StringComparison.OrdinalIgnoreCase));

                if (recipe != null)
                {
                    ClearData(recipe);
                }
                else
                {
                    //messagebox to show that the recipe was not found
                    MessageBox.Show("No Recipe Found!");
                }
            }
            else
            {
                MessageBox.Show("Please enter a recipe!");
            }
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            //clear fields button

            //clearing recipename textbox
            RecipeNameTb.Clear();

            //closing richtextbox
            recipeRichTextBox.Document.Blocks.Clear();
            recipeRichTextBox.Visibility = Visibility.Collapsed;

            //clearing the textbox lists
            ingredientTextBoxes.Clear();
            stepTextBoxes.Clear();

            //removing other textboxes
            foreach (StackPanel sp in stackPanels)
            {
                sp.Children.Clear();
            }
        }
        #endregion

        #region ScaleRecipe
        private void ScaleRecipe(Recipe recipe)
        {
            //scale recipe button
            //taking user input using interaction inputbox and parsing the factor number
            string userInput = Interaction.InputBox("Please enter 0.5, 2, or 3 to scale your recipe...", "Scale Recipe", "'2'");
            double factor;
            if (double.TryParse(userInput, out factor))
            {
                if (factor == 0.5 || factor == 1 || factor == 2 || factor == 3)
                {
                    foreach (var ingredient in recipe.Ingredients)
                    {
                        ingredient.Quantity *= factor;
                        ingredient.Calories *= factor;
                        ingredient.ScaleFactor = factor;
                    }
                    Console.WriteLine("\nSuccessfully scaled the recipe!");

                    //displaying the newly scaled recipe
                    recipeRichTextBox.Document.Blocks.Clear();
                    Display(recipe);
                }
                else
                {
                    //messagebox to show that input was a invalid number
                    MessageBox.Show("You entered the wrong scale factor number...");
                }
            }
            else
            {
                //messagebox to show that input was a invalid number
                MessageBox.Show("Please input a valid number!");
            }

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //scale recipe button
            //checking for recipes to work with first
            if (recipes.Count > 0)
            {
                //taking user input using RecipeListCombo.SelectedItem and searching for the recipe.
                string userInput = RecipeListCombo.SelectedItem.ToString();
                Recipe recipe = recipes.FirstOrDefault(r => r.recipeName.Equals(userInput, StringComparison.OrdinalIgnoreCase));

                if (recipe != null)
                {
                    ScaleRecipe(recipe);
                }
                else
                {
                    //messagebox to show that the recipe was not found
                    MessageBox.Show("No Recipe Found!");
                }
            }
            else
            {
                MessageBox.Show("Please enter a recipe!");
            }
        }
        #endregion

        #region ResetQuantities
        private void ResetQuantities(Recipe recipe)
        {
            foreach (var ingredient in recipe.Ingredients)
            {
                ingredient.Quantity /= ingredient.ScaleFactor;
                ingredient.Calories /= ingredient.ScaleFactor;
                ingredient.ScaleFactor = 1;
            }

            //displaying the newly resetted recipe
            recipeRichTextBox.Document.Blocks.Clear();
            Display(recipe);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (recipes.Count > 0)
            {
                //taking user input using RecipeListCombo.SelectedItem and searching for the recipe.
                string userInput = RecipeListCombo.SelectedItem.ToString();
                Recipe recipe = recipes.FirstOrDefault(r => r.recipeName.Equals(userInput, StringComparison.OrdinalIgnoreCase));

                if (recipe != null)
                {
                    ResetQuantities(recipe);
                }
                else
                {
                    //messagebox to show that the recipe was not found
                    MessageBox.Show("No Recipe Found!");
                }
            }
            else
            {
                MessageBox.Show("Please enter a recipe!");
            }
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

        public void exceedTotalCalories(Recipe recipe)
        {
            if (TotalCalories(recipe) > 300)
            {
                //printing warning if it exceeds 300 calories
                recipeRichTextBox.AppendText($"WARNING: Total calories for {recipe.recipeName} exceed 300!");

            }
        }

        #endregion

        #region FilterBy
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (recipes.Count > 0)
            {
                //filter by Ingredient Button
                //creating a list to store all the recipes that have the same ingredient
                List<string> selectedRecipeNames = new List<string>();

                //hiding the richtextbox as its not needed
                recipeRichTextBox.Visibility = Visibility.Collapsed;

                //the user input interaction box
                string userInput = Interaction.InputBox("Please enter an ingredient to search for...", "Search By Ingredient");

                //boolean for the if function to tell the user the found matching recipes
                bool ingredientFound = false;

                //checking is the user selected an item
                if (userInput != "")
                {

                    //searching for the matching ingredient
                    foreach (Recipe recipe in recipes)
                    {
                        //if found, it will set the boolean to true and add the recipe to the list
                        if (recipe.Ingredients.Any(i => i.Name.Equals(userInput, StringComparison.OrdinalIgnoreCase)))
                        {
                            ingredientFound = true;
                            selectedRecipeNames.Add(recipe.recipeName);
                        }
                    }

                    if (ingredientFound)
                    {
                        //taking the recipe names and storing them to a string to print in the messagebox
                        string recipeNamesString = string.Join(", ", selectedRecipeNames);

                        //printing the results in a messagebox
                        MessageBox.Show($"Recipes with ingredient '{userInput}': {recipeNamesString}");
                    }
                    else
                    {
                        MessageBox.Show("There are no recipes with that ingredient.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter a recipe!");
            }
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            if (recipes.Count > 0)
            {
                // Filter by Food Group Button
                // Create a list to store the recipes that have the specified food group
                List<string> selectedRecipeNames = new List<string>();

                // Hide the RichTextBox as it is not needed
                recipeRichTextBox.Visibility = Visibility.Collapsed;

                // Create an instance of the FoodGroupInputDialog window
                FoodGroupInputDialog foodGroupDialog = new FoodGroupInputDialog();

                // Display the dialog window and wait for the user to make a selection
                foodGroupDialog.ShowDialog();

                // Retrieve the selected food group from the dialog
                string userInput = foodGroupDialog.SelectedFoodGroup;

                // Boolean flag to indicate if recipes with the specified food group were found
                bool foodGroupFound = false;

                //checking is the user selected an item
                if (userInput != null)
                {
                    // Search for recipes with the matching food group
                    foreach (Recipe recipe in recipes)
                    {
                        // If the recipe has at least one ingredient with the specified food group, add it to the list
                        if (recipe.Ingredients.Any(i => i.FoodGroup.Equals(userInput, StringComparison.OrdinalIgnoreCase)))
                        {
                            foodGroupFound = true;
                            selectedRecipeNames.Add(recipe.recipeName);
                        }
                    }

                    if (foodGroupFound)
                    {
                        // Convert the recipe names to a string for display in the MessageBox
                        string recipeNamesString = string.Join(", ", selectedRecipeNames);

                        // Show the results in a MessageBox
                        MessageBox.Show($"Recipes with food group '{userInput}': {recipeNamesString}");
                    }
                    else
                    {
                        MessageBox.Show($"There are no recipes with '{userInput}'.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter a recipe!");
            }
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            if (recipes.Count > 0)
            {
                // Filter by Ingredient Button
                // Create a list to store the recipes that have the specified ingredient
                List<string> selectedRecipeNames = new List<string>();

                // Hide the RichTextBox as it is not needed
                recipeRichTextBox.Visibility = Visibility.Collapsed;

                // Boolean flag to indicate if recipes with the specified ingredient were found
                bool ingredientFound = false;

                // Prompt the user to enter a maximum number of calories
                double maxCalories;

                string userInput = Interaction.InputBox("Please enter the maximum number of calories:", "Set Maximum Calories");

                if (userInput != "")
                {
                    //testing the user input using tryparse
                    if (double.TryParse(userInput, out maxCalories))
                    {

                        // Search for recipes with the matching ingredient and maximum calories
                        foreach (Recipe recipe in recipes)
                        {
                            // Check if the recipe has the specified ingredient and does not exceed the maximum calories
                            if (recipe.Ingredients.Sum(i => i.Calories) <= maxCalories)
                            {
                                ingredientFound = true;
                                selectedRecipeNames.Add(recipe.recipeName);
                            }
                        }

                        if (ingredientFound)
                        {
                            // Convert the recipe names to a string for display in the MessageBox
                            string recipeNamesString = string.Join(", ", selectedRecipeNames);

                            // Show the results in a MessageBox
                            MessageBox.Show($"Recipes with a maximum calories of '{maxCalories}': {recipeNamesString}");
                        }
                        else
                        {
                            MessageBox.Show($"There are no recipes within '{maxCalories}' maximum calories.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid maximum calories. Please enter a valid number.");
                    }

                }
            }
            else
            {
                MessageBox.Show("Please enter a recipe!");
            }
        }
        #endregion
    }
}

