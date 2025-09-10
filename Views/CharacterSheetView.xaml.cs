using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media; // Required for SolidColorBrush
using System.Windows.Navigation;
using DDGameMaster.Models.Character;
using DDGameMaster.Models.Game;

namespace DDGameMaster.Views
{
    public partial class CharacterSheetView : Page
    {
        private readonly int[] xpThresholds = { 0, 300, 900, 2700, 6500, 14000, 23000, 34000, 48000, 64000, 85000, 100000, 120000, 140000, 165000, 195000, 225000, 265000, 305000, 355000 };

        public CharacterSheetView()
        {
            InitializeComponent();
            LoadCharacterData();
        }

        private void LoadCharacterData()
        {
            var character = GameState.Instance.PlayerCharacter;
            if (character != null)
            {
                UpdateAllDisplays();
            }
            else
            {
                NameTextBlock.Text = "No Character Found";
            }
        }
        
        private void UpdateAllDisplays()
        {
            var character = GameState.Instance.PlayerCharacter;
            if (character == null) return;

            NameTextBlock.Text = character.Name;
            RaceClassLevelTextBlock.Text = $"Level {character.Level} {character.Race.Name} {character.Class.Name}";
            ArmorClassTextBlock.Text = $"AC: {character.ArmorClass}";
            HitPointsTextBlock.Text = $"HP: {character.CurrentHitPoints} / {character.MaximumHitPoints}";
            
            int nextLevelXp = (character.Level < xpThresholds.Length) ? xpThresholds[character.Level] : 0;
            ExperienceTextBlock.Text = $"XP: {character.ExperiencePoints} / {nextLevelXp}";
            
            InventoryListBox.ItemsSource = null;
            InventoryListBox.ItemsSource = character.Inventory;

            UpdateSavingThrowsDisplay(character);
            UpdateSkillsDisplay(character); // NEW: Update the skills display
        }

        private void UpdateSavingThrowsDisplay(Character character)
        {
            int proficiencyBonus = (character.Level - 1) / 4 + 2;

            int strMod = CharacterStats.GetModifier(character.Stats.Strength);
            int dexMod = CharacterStats.GetModifier(character.Stats.Dexterity);
            int conMod = CharacterStats.GetModifier(character.Stats.Constitution);
            int intMod = CharacterStats.GetModifier(character.Stats.Intelligence);
            int wisMod = CharacterStats.GetModifier(character.Stats.Wisdom);
            int chaMod = CharacterStats.GetModifier(character.Stats.Charisma);

            int strSave = strMod + (character.SavingThrowProficiencies.Contains("Strength") ? proficiencyBonus : 0);
            int dexSave = dexMod + (character.SavingThrowProficiencies.Contains("Dexterity") ? proficiencyBonus : 0);
            int conSave = conMod + (character.SavingThrowProficiencies.Contains("Constitution") ? proficiencyBonus : 0);
            int intSave = intMod + (character.SavingThrowProficiencies.Contains("Intelligence") ? proficiencyBonus : 0);
            int wisSave = wisMod + (character.SavingThrowProficiencies.Contains("Wisdom") ? proficiencyBonus : 0);
            int chaSave = chaMod + (character.SavingThrowProficiencies.Contains("Charisma") ? proficiencyBonus : 0);

            StrSaveTextBlock.Text = $"Strength:     {(strSave >= 0 ? "+" : "")}{strSave}";
            DexSaveTextBlock.Text = $"Dexterity:    {(dexSave >= 0 ? "+" : "")}{dexSave}";
            ConSaveTextBlock.Text = $"Constitution: {(conSave >= 0 ? "+" : "")}{conSave}";
            IntSaveTextBlock.Text = $"Intelligence: {(intSave >= 0 ? "+" : "")}{intSave}";
            WisSaveTextBlock.Text = $"Wisdom:       {(wisSave >= 0 ? "+" : "")}{wisSave}";
            ChaSaveTextBlock.Text = $"Charisma:     {(chaSave >= 0 ? "+" : "")}{chaSave}";
        }

        // NEW METHOD to calculate and display all 18 skills
        private void UpdateSkillsDisplay(Character character)
        {
            SkillsGrid.Children.Clear();
            SkillsGrid.RowDefinitions.Clear();
            SkillsGrid.ColumnDefinitions.Clear();

            SkillsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            SkillsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            var skills = new List<(string Name, string Stat)>
            {
                ("Acrobatics", "Dexterity"), ("Animal Handling", "Wisdom"), ("Arcana", "Intelligence"),
                ("Athletics", "Strength"), ("Deception", "Charisma"), ("History", "Intelligence"),
                ("Insight", "Wisdom"), ("Intimidation", "Charisma"), ("Investigation", "Intelligence"),
                ("Medicine", "Wisdom"), ("Nature", "Intelligence"), ("Perception", "Wisdom"),
                ("Performance", "Charisma"), ("Persuasion", "Charisma"), ("Religion", "Intelligence"),
                ("Sleight of Hand", "Dexterity"), ("Stealth", "Dexterity"), ("Survival", "Wisdom")
            };

            int proficiencyBonus = (character.Level - 1) / 4 + 2;

            for (int i = 0; i < skills.Count; i++)
            {
                if (i % 9 == 0 && i != 0) { } // Simple logic to split into two columns
                if(SkillsGrid.RowDefinitions.Count <= i % 9)
                {
                    SkillsGrid.RowDefinitions.Add(new RowDefinition());
                }

                var skill = skills[i];
                int statValue = 0;
                switch (skill.Stat)
                {
                    case "Strength": statValue = character.Stats.Strength; break;
                    case "Dexterity": statValue = character.Stats.Dexterity; break;
                    case "Constitution": statValue = character.Stats.Constitution; break;
                    case "Intelligence": statValue = character.Stats.Intelligence; break;
                    case "Wisdom": statValue = character.Stats.Wisdom; break;
                    case "Charisma": statValue = character.Stats.Charisma; break;
                }

                int modifier = CharacterStats.GetModifier(statValue);
                bool isProficient = character.SkillProficiencies.Contains(skill.Name);
                int totalBonus = modifier + (isProficient ? proficiencyBonus : 0);

                var textBlock = new TextBlock
                {
                    Text = $"{skill.Name}: {(totalBonus >= 0 ? "+" : "")}{totalBonus}",
                    Foreground = isProficient ? new SolidColorBrush(Colors.LawnGreen) : new SolidColorBrush(Colors.White),
                    Margin = new Thickness(5, 2, 5, 2)
                };

                Grid.SetRow(textBlock, i % 9);
                Grid.SetColumn(textBlock, i / 9);
                SkillsGrid.Children.Add(textBlock);
            }
        }

        private void Damage_Click(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(HpAmountTextBox.Text, out int amount))
            {
                var character = GameState.Instance.PlayerCharacter;
                if (character != null)
                {
                    character.CurrentHitPoints -= amount;
                    if (character.CurrentHitPoints < 0) character.CurrentHitPoints = 0;
                    UpdateAllDisplays();
                }
            }
        }

        private void Heal_Click(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(HpAmountTextBox.Text, out int amount))
            {
                var character = GameState.Instance.PlayerCharacter;
                if (character != null)
                {
                    character.CurrentHitPoints += amount;
                    if (character.CurrentHitPoints > character.MaximumHitPoints) character.CurrentHitPoints = character.MaximumHitPoints;
                    UpdateAllDisplays();
                }
            }
        }
        
        private void AddXp_Click(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(XpAmountTextBox.Text, out int amount))
            {
                var character = GameState.Instance.PlayerCharacter;
                if (character != null)
                {
                    character.ExperiencePoints += amount;
                    CheckForLevelUp(character);
                    UpdateAllDisplays();
                }
            }
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            var character = GameState.Instance.PlayerCharacter;
            string newItem = ItemNameTextBox.Text;

            if (character != null && !string.IsNullOrWhiteSpace(newItem))
            {
                character.Inventory.Add(newItem);
                ItemNameTextBox.Clear();
                UpdateAllDisplays();
            }
        }

        private void CheckForLevelUp(Character character)
        {
            if (character.Level >= xpThresholds.Length) return;

            int nextLevelXp = xpThresholds[character.Level];
            if (character.ExperiencePoints >= nextLevelXp)
            {
                character.Level++;
                
                int conModifier = CharacterStats.GetModifier(character.Stats.Constitution);
                int newHp = (character.Class.HitDie / 2) + 1 + conModifier;
                if (newHp < 1) newHp = 1;
                
                character.MaximumHitPoints += newHp;
                character.CurrentHitPoints += newHp;

                MessageBox.Show($"Congratulations! You have reached Level {character.Level}!");
                
                CheckForLevelUp(character);
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