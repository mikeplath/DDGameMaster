using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DDGameMaster.Models.Character;
using DDGameMaster.Models.Game;

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
            try
            {
                Character newCharacter = new Character();

                newCharacter.Name = NameTextBox.Text;
                newCharacter.Race.Name = RaceTextBox.Text;
                newCharacter.Class.Name = ClassTextBox.Text;
                
                // NEW: Read the stats from the text boxes and save them
                newCharacter.Stats.Strength = int.Parse(StrengthTextBox.Text);
                newCharacter.Stats.Dexterity = int.Parse(DexterityTextBox.Text);
                newCharacter.Stats.Constitution = int.Parse(ConstitutionTextBox.Text);
                newCharacter.Stats.Intelligence = int.Parse(IntelligenceTextBox.Text);
                newCharacter.Stats.Wisdom = int.Parse(WisdomTextBox.Text);
                newCharacter.Stats.Charisma = int.Parse(CharismaTextBox.Text);

                // Store the completed character in our central GameState
                GameState.Instance.PlayerCharacter = newCharacter;

                MessageBox.Show($"Character '{newCharacter.Name}' was created and stored in GameState.");

                if (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();
                }
            }
            catch (FormatException)
            {
                // This is simple error handling. If you type "abc" into a stat box, this message will appear.
                MessageBox.Show("Invalid input. Please ensure all stats are numbers.");
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