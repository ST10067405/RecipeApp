using System.Windows.Controls;

namespace RecipeApp3
{
    public class Step
    {
        public string Steps { get; set; }
        public Step(TextBox stepsTextBox)
        {
            StepsTextBox = stepsTextBox;
        }

        public TextBox StepsTextBox { get; set; }

        public Step(string steps)
        {
            Steps = steps;
        }
    }
}