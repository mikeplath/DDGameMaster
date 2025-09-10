using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DDGameMaster.Models.Character; 
using DDGameMaster.Models.Game; // This line tells the script to use our new GameState manager

namespace DDGameMaster.Views
{
    public partial class CharacterCreationView : Page
    {
        public CharacterCreationView()
        {
            InitializeComponent();
        }

        private void CreateCharacter_Click(object sender, RoutedEventArgs e)
        {
            // Create a new Character object using our blueprint
            Character newCharacter = new Character();

            // Fill the blueprint with data from the text boxes on the screen
            newCharacter.Name = NameTextBox.Text;
            newCharacter.Race.Name = RaceTextBox.Text;
            newCharacter.Class.Name = ClassTextBox.Text;

            // **THIS IS THE NEW, IMPORTANT PART**
            // We are now storing the character we just made into our central GameState manager.
            GameState.Instance.PlayerCharacter = newCharacter;

            // Display a message box to confirm that the character was created AND stored.
            MessageBox.Show($"Character '{newCharacter.Name}' was created and stored in GameState.");

            // After creation, navigate back to the previous screen (the main menu)
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            // This allows the "Back" button to work
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}