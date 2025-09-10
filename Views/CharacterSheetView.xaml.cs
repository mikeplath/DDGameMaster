using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DDGameMaster.Models.Game; // This tells the script to use our GameState manager

namespace DDGameMaster.Views
{
    public partial class CharacterSheetView : Page
    {
        public CharacterSheetView()
        {
            InitializeComponent();
            LoadCharacterData(); // We call a new method to load the data when the page opens
        }

        private void LoadCharacterData()
        {
            // Get the character from our central storage box (GameState)
            var character = GameState.Instance.PlayerCharacter;

            // Check to make sure a character actually exists before we try to display it
            if (character != null)
            {
                // Take the data from the stored character and put it into the text boxes on the screen
                NameTextBlock.Text = $"Name: {character.Name}";
                RaceTextBlock.Text = $"Race: {character.Race.Name}";
                ClassTextBlock.Text = $"Class: {character.Class.Name}";
            }
            else
            {
                // If for some reason no character is found, display a message.
                NameTextBlock.Text = "No character found.";
                RaceTextBlock.Text = "Please create a character first.";
                ClassTextBlock.Text = "";
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