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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RecipeApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Recipes(object sender, RoutedEventArgs e)
        {
            RecipeWindow rw = new RecipeWindow();
            rw.Show();
            this.Close();
        }

        private void AddRecipes(object sender, RoutedEventArgs e)
        {

        }

        private void EditRecipes(object sender, RoutedEventArgs e)
        {
            //AddRecipeWindow rw = new AddRecipeWindow();
           // rw.Show();
            //this.Close();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            //Closes the app
            this.Close();
        }


    }
}
