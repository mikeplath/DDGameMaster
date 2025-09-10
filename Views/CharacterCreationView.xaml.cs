using System;
using System.Collections.Generic;
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
            PopulateComboBoxes();
        }

        private void PopulateComboBoxes()
        {
            var races = new List<string> { "Human", "Elf", "Dwarf", "Halfling", "Dragonborn", "Gnome", "Half-Elf", "Half-Orc", "Tiefling" };
            RaceComboBox.ItemsSource = races;
            RaceComboBox.SelectedIndex = 0;

            var classes = new List<string> { "Barbarian", "Bard", "Cleric", "Druid", "Fighter", "Monk", "Paladin", "Ranger", "Rogue", "Sorcerer", "Warlock", "Wizard" };
            ClassComboBox.ItemsSource = classes;
            ClassComboBox.SelectedIndex = 0;
        }

        private void CreateCharacter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Character newCharacter = new Character();

                newCharacter.Name = NameTextBox.Text;
                newCharacter.Race.Name = RaceComboBox.SelectedItem.ToString();
                
                // Set Class Name
                string className = ClassComboBox.SelectedItem.ToString();
                newCharacter.Class.Name = className;

                // UPDATED LOGIC: Set HitDie based on selected class
                switch (className)
                {
                    case "Barbarian": newCharacter.Class.HitDie = 12; break;
                    case "Fighter":
                    case "Paladin":
                    case "Ranger": newCharacter.Class.HitDie = 10; break;
                    case "Bard":
                    case "Cleric":
                    case "Druid":
                    case "Monk":
                    case "Rogue":
                    case "Warlock": newCharacter.Class.HitDie = 8; break;
                    case "Sorcerer":
                    case "Wizard": newCharacter.Class.HitDie = 6; break;
                }
                
                // Read stats from text boxes
                newCharacter.Stats.Strength = int.Parse(StrengthTextBox.Text);
                newCharacter.Stats.Dexterity = int.Parse(DexterityTextBox.Text);
                newCharacter.Stats.Constitution = int.Parse(ConstitutionTextBox.Text);
                newCharacter.Stats.Intelligence = int.Parse(IntelligenceTextBox.Text);
                newCharacter.Stats.Wisdom = int.Parse(WisdomTextBox.Text);
                newCharacter.Stats.Charisma = int.Parse(CharismaTextBox.Text);

                // NEW: Calculate starting Hit Points
                int conModifier = CharacterStats.GetModifier(newCharacter.Stats.Constitution);
                newCharacter.MaximumHitPoints = newCharacter.Class.HitDie + conModifier;
                newCharacter.CurrentHitPoints = newCharacter.MaximumHitPoints; // Start with full health

                GameState.Instance.PlayerCharacter = newCharacter;

                MessageBox.Show($"Character '{newCharacter.Name}' was created with {newCharacter.MaximumHitPoints} HP.");

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