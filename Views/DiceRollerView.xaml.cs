using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace DDGameMaster.Views
{
    public partial class DiceRollerView : Page
    {
        private Random random = new Random();

        public DiceRollerView()
        {
            InitializeComponent();
        }

        private void Roll_Click(object sender, RoutedEventArgs e)
        {
            // Get the button that was clicked
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                // The 'Tag' property holds the number of sides for the die (4, 6, 8, etc.)
                int sides = Convert.ToInt32(clickedButton.Tag);
                
                // Generate a random number between 1 and the number of sides (inclusive)
                int rollResult = random.Next(1, sides + 1);

                // Update the text block to show the result
                ResultTextBlock.Text = $"Result: {rollResult}";
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}