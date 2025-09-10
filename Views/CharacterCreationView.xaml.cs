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
            RaceComboBox.ItemsSource = races; RaceComboBox.SelectedIndex = 0;
            var classes = new List<string> { "Barbarian", "Bard", "Cleric", "Druid", "Fighter", "Monk", "Paladin", "Ranger", "Rogue", "Sorcerer", "Warlock", "Wizard" };
            ClassComboBox.ItemsSource = classes; ClassComboBox.SelectedIndex = 0;
            var alignments = new List<string> { "Lawful Good", "Neutral Good", "Chaotic Good", "Lawful Neutral", "True Neutral", "Chaotic Neutral", "Lawful Evil", "Neutral Evil", "Chaotic Evil" };
            AlignmentComboBox.ItemsSource = alignments; AlignmentComboBox.SelectedItem = "True Neutral";
        }

        private void CreateCharacter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Character newCharacter = new Character();
                newCharacter.Name = NameTextBox.Text;
                newCharacter.Race.Name = RaceComboBox.SelectedItem.ToString();
                newCharacter.Class.Name = ClassComboBox.SelectedItem.ToString();
                newCharacter.Alignment = AlignmentComboBox.SelectedItem.ToString();
                newCharacter.Appearance = AppearanceTextBox.Text;
                newCharacter.Backstory = BackstoryTextBox.Text;

                // NEW: Save Personality Details
                newCharacter.PersonalityTraits = PersonalityTraitsTextBox.Text;
                newCharacter.Ideals = IdealsTextBox.Text;
                newCharacter.Bonds = BondsTextBox.Text;
                newCharacter.Flaws = FlawsTextBox.Text;

                switch (newCharacter.Race.Name)
                {
                    case "Elf": newCharacter.Abilities.Add("Darkvision"); newCharacter.Abilities.Add("Fey Ancestry"); break;
                    case "Dwarf": newCharacter.Abilities.Add("Darkvision"); newCharacter.Abilities.Add("Dwarven Resilience"); break;
                    case "Halfling": newCharacter.Abilities.Add("Lucky"); newCharacter.Abilities.Add("Brave"); break;
                    case "Dragonborn": newCharacter.Abilities.Add("Draconic Ancestry"); newCharacter.Abilities.Add("Breath Weapon"); break;
                    case "Gnome": newCharacter.Abilities.Add("Darkvision"); newCharacter.Abilities.Add("Gnome Cunning"); break;
                    case "Half-Orc": newCharacter.Abilities.Add("Darkvision"); newCharacter.Abilities.Add("Relentless Endurance"); break;
                    case "Tiefling": newCharacter.Abilities.Add("Darkvision"); newCharacter.Abilities.Add("Hellish Resistance"); break;
                }
                switch (newCharacter.Class.Name)
                {
                    case "Barbarian": newCharacter.Class.HitDie = 12; newCharacter.SavingThrowProficiencies.AddRange(new[] { "Strength", "Constitution" }); newCharacter.Abilities.Add("Rage"); break;
                    case "Bard": newCharacter.Class.HitDie = 8; newCharacter.SavingThrowProficiencies.AddRange(new[] { "Dexterity", "Charisma" }); newCharacter.Abilities.Add("Bardic Inspiration"); break;
                    case "Cleric": newCharacter.Class.HitDie = 8; newCharacter.SavingThrowProficiencies.AddRange(new[] { "Wisdom", "Charisma" }); newCharacter.Abilities.Add("Spellcasting"); newCharacter.Abilities.Add("Divine Domain"); break;
                    case "Druid": newCharacter.Class.HitDie = 8; newCharacter.SavingThrowProficiencies.AddRange(new[] { "Intelligence", "Wisdom" }); newCharacter.Abilities.Add("Wild Shape"); break;
                    case "Fighter": newCharacter.Class.HitDie = 10; newCharacter.SavingThrowProficiencies.AddRange(new[] { "Strength", "Constitution" }); newCharacter.Abilities.Add("Second Wind"); break;
                    case "Monk": newCharacter.Class.HitDie = 8; newCharacter.SavingThrowProficiencies.AddRange(new[] { "Strength", "Dexterity" }); newCharacter.Abilities.Add("Unarmored Defense"); newCharacter.Abilities.Add("Martial Arts"); break;
                    case "Paladin": newCharacter.Class.HitDie = 10; newCharacter.SavingThrowProficiencies.AddRange(new[] { "Wisdom", "Charisma" }); newCharacter.Abilities.Add("Divine Sense"); newCharacter.Abilities.Add("Lay on Hands"); break;
                    case "Ranger": newCharacter.Class.HitDie = 10; newCharacter.SavingThrowProficiencies.AddRange(new[] { "Strength", "Dexterity" }); newCharacter.Abilities.Add("Favored Enemy"); break;
                    case "Rogue": newCharacter.Class.HitDie = 8; newCharacter.SavingThrowProficiencies.AddRange(new[] { "Dexterity", "Intelligence" }); newCharacter.Abilities.Add("Sneak Attack"); break;
                    case "Sorcerer": newCharacter.Class.HitDie = 6; newCharacter.SavingThrowProficiencies.AddRange(new[] { "Constitution", "Charisma" }); newCharacter.Abilities.Add("Spellcasting"); newCharacter.Abilities.Add("Sorcerous Origin"); break;
                    case "Warlock": newCharacter.Class.HitDie = 8; newCharacter.SavingThrowProficiencies.AddRange(new[] { "Wisdom", "Charisma" }); newCharacter.Abilities.Add("Pact Magic"); break;
                    case "Wizard": newCharacter.Class.HitDie = 6; newCharacter.SavingThrowProficiencies.AddRange(new[] { "Intelligence", "Wisdom" }); newCharacter.Abilities.Add("Arcane Recovery"); break;
                }
                newCharacter.Stats.Strength = int.Parse(StrengthTextBox.Text); newCharacter.Stats.Dexterity = int.Parse(DexterityTextBox.Text); newCharacter.Stats.Constitution = int.Parse(ConstitutionTextBox.Text); newCharacter.Stats.Intelligence = int.Parse(IntelligenceTextBox.Text); newCharacter.Stats.Wisdom = int.Parse(WisdomTextBox.Text); newCharacter.Stats.Charisma = int.Parse(CharismaTextBox.Text);
                int conModifier = CharacterStats.GetModifier(newCharacter.Stats.Constitution); newCharacter.MaximumHitPoints = newCharacter.Class.HitDie + conModifier; newCharacter.CurrentHitPoints = newCharacter.MaximumHitPoints;
                int dexModifier = CharacterStats.GetModifier(newCharacter.Stats.Dexterity); newCharacter.ArmorClass = 10 + dexModifier;
                if (AcrobaticsCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Acrobatics"); if (AnimalHandlingCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Animal Handling"); if (ArcanaCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Arcana"); if (AthleticsCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Athletics"); if (DeceptionCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Deception"); if (HistoryCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("History"); if (InsightCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Insight"); if (IntimidationCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Intimidation"); if (InvestigationCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Investigation"); if (MedicineCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Medicine"); if (NatureCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Nature"); if (PerceptionCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Perception"); if (PerformanceCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Performance"); if (PersuasionCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Persuasion"); if (ReligionCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Religion"); if (SleightOfHandCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Sleight of Hand"); if (StealthCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Stealth"); if (SurvivalCheckBox.IsChecked == true) newCharacter.SkillProficiencies.Add("Survival");
                GameState.Instance.PlayerCharacter = newCharacter;
                MessageBox.Show($"Character '{newCharacter.Name}' was created.");
                if (NavigationService.CanGoBack) { NavigationService.GoBack(); }
            }
            catch (FormatException) { MessageBox.Show("Invalid input. Please ensure all stats are numbers."); }
            catch (Exception ex) { MessageBox.Show($"An error occurred: {ex.Message}"); }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack) { NavigationService.GoBack(); }
        }
    }
}