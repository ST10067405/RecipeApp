using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RecipeApp3
{
    /// <summary>
    /// Interaction logic for FoodGroupInputDialog.xaml
    /// </summary>
    public partial class FoodGroupInputDialog : Window
    {
        // Property to store the selected food group
        public string SelectedFoodGroup { get; private set; }

        public FoodGroupInputDialog()
        {
            InitializeComponent();
        }

        // Event handler for food group button clicks
        private void FoodGroupButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            SelectedFoodGroup = button.Content.ToString();

            // Close the dialog window
            Close();
        }
    }
}
