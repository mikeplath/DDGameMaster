using System;
using System.Collections.Generic; // Required for using lists
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
            // NEW: Call the method to populate our dropdowns
            PopulateComboBoxes();
        }

        // NEW METHOD: This sets up the dropdown menus with choices.
        private void PopulateComboBoxes()
        {
            // Create a list of available races
            var races = new List<string> { "Human", "Elf", "Dwarf", "Halfling", "Dragonborn", "Gnome", "Half-Elf", "Half-Orc", "Tiefling" };
            RaceComboBox.ItemsSource = races;
            RaceComboBox.SelectedIndex = 0; // Select the first item by default

            // Create a list of available classes
            var classes = new List<string> { "Barbarian", "Bard", "Cleric", "Druid", "Fighter", "Monk", "Paladin", "Ranger", "Rogue", "Sorcerer", "Warlock", "Wizard" };
            ClassComboBox.ItemsSource = classes;
            ClassComboBox.SelectedIndex = 0; // Select the first item by default
        }

        private void CreateCharacter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Character newCharacter = new Character();

                newCharacter.Name = NameTextBox.Text;

                // UPDATED: Get the selected race and class from the dropdowns
                newCharacter.Race.Name = RaceComboBox.SelectedItem.ToString();
                newCharacter.Class.Name = ClassComboBox.SelectedItem.ToString();
                
                newCharacter.Stats.Strength = int.Parse(StrengthTextBox.Text);
                newCharacter.Stats.Dexterity = int.Parse(DexterityTextBox.Text);
                newCharacter.Stats.Constitution = int.Parse(ConstitutionTextBox.Text);
                newCharacter.Stats.Intelligence = int.Parse(IntelligenceTextBox.Text);
                newCharacter.Stats.Wisdom = int.Parse(WisdomTextBox.Text);
                newCharacter.Stats.Charisma = int.Parse(CharismaTextBox.Text);

                GameState.Instance.PlayerCharacter = newCharacter;

                MessageBox.Show($"Character '{newCharacter.Name}' was created and stored in GameState.");

                if (NavigationService.CanGoBack)
                {
                    NavigationService.GoBack();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid input. Please ensure all stats are numbers.");
            }
            catch (Exception ex)
            {
                // A general catch-all for any other unexpected errors
                MessageBox.Show($"An error occurred: {ex.Message}");
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