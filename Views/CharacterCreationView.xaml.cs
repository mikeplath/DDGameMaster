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
                
                string className = ClassComboBox.SelectedItem.ToString();
                newCharacter.Class.Name = className;

                switch (className)
                {
                    case "Barbarian": newCharacter.Class.HitDie = 12; newCharacter.SavingThrowProficiencies.AddRange(new[] { "Strength", "Constitution" }); break;
                    case "Bard": newCharacter.Class.HitDie = 8; newCharacter.SavingThrowProficiencies.AddRange(new[] { "Dexterity", "Charisma" }); break;
                    case "Cleric": newCharacter.Class.HitDie = 8; newCharacter.SavingThrowProficiencies.AddRange(new[] { "Wisdom", "Charisma" }); break;
                    case "Druid": newCharacter.Class.HitDie = 8; newCharacter.SavingThrowProficiencies.AddRange(new[] { "Intelligence", "Wisdom" }); break;
                    case "Fighter": newCharacter.Class.HitDie = 10; newCharacter.SavingThrowProficiencies.AddRange(new[] { "Strength", "Constitution" }); break;
                    case "Monk": newCharacter.Class.HitDie = 8; newCharacter.SavingThrowProficiencies.AddRange(new[] { "Strength", "Dexterity" }); break;
                    case "Paladin": newCharacter.Class.HitDie = 10; newCharacter.SavingThrowProficiencies.AddRange(new[] { "Wisdom", "Charisma" }); break;
                    case "Ranger": newCharacter.Class.HitDie = 10; newCharacter.SavingThrowProficiencies.AddRange(new[] { "Strength", "Dexterity" }); break;
                    case "Rogue": newCharacter.Class.HitDie = 8; newCharacter.SavingThrowProficiencies.AddRange(new[] { "Dexterity", "Intelligence" }); break;
                    case "Sorcerer": newCharacter.Class.HitDie = 6; newCharacter.SavingThrowProficiencies.AddRange(new[] { "Constitution", "Charisma" }); break;
                    case "Warlock": newCharacter.Class.HitDie = 8; newCharacter.SavingThrowProficiencies.AddRange(new[] { "Wisdom", "Charisma" }); break;
                    case "Wizard": newCharacter.Class.HitDie = 6; newCharacter.SavingThrowProficiencies.AddRange(new[] { "Intelligence", "Wisdom" }); break;
                }
                
                newCharacter.Stats.Strength = int.Parse(StrengthTextBox.Text);
                newCharacter.Stats.Dexterity = int.Parse(DexterityTextBox.Text);
                newCharacter.Stats.Constitution = int.Parse(ConstitutionTextBox.Text);
                newCharacter.Stats.Intelligence = int.Parse(IntelligenceTextBox.Text);
                newCharacter.Stats.Wisdom = int.Parse(WisdomTextBox.Text);
                newCharacter.Stats.Charisma = int.Parse(CharismaTextBox.Text);

                int conModifier = CharacterStats.GetModifier(newCharacter.Stats.Constitution);
                newCharacter.MaximumHitPoints = newCharacter.Class.HitDie + conModifier;
                newCharacter.CurrentHitPoints = newCharacter.MaximumHitPoints;

                int dexModifier = CharacterStats.GetModifier(newCharacter.Stats.Dexterity);
                newCharacter.ArmorClass = 10 + dexModifier;

                // NEW: Check all skill checkboxes and add the proficient ones to the character
                if (AcrobaticsCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Acrobatics");
                if (AnimalHandlingCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Animal Handling");
                if (ArcanaCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Arcana");
                if (AthleticsCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Athletics");
                if (DeceptionCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Deception");
                if (HistoryCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("History");
                if (InsightCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Insight");
                if (IntimidationCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Intimidation");
                if (InvestigationCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Investigation");
                if (MedicineCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Medicine");
                if (NatureCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Nature");
                if (PerceptionCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Perception");
                if (PerformanceCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Performance");
                if (PersuasionCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Persuasion");
                if (ReligionCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Religion");
                if (SleightOfHandCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Sleight of Hand");
                if (StealthCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Stealth");
                if (SurvivalCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Survival");

                GameState.Instance.PlayerCharacter = newCharacter;

                MessageBox.Show($"Character '{newCharacter.Name}' was created.");

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