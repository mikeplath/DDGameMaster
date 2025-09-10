using System;
using System.Windows;
using System.Windows.Controls;
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
                RaceClassLevelTextBlock.Text = "Please create a new character from the main menu.";
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
            
            // UPDATED: Refresh the inventory list display
            InventoryListBox.ItemsSource = null; // Clear the list first
            InventoryListBox.ItemsSource = character.Inventory; // Then set it to the character's inventory

            int strMod = CharacterStats.GetModifier(character.Stats.Strength);
            int dexMod = CharacterStats.GetModifier(character.Stats.Dexterity);
            int conMod = CharacterStats.GetModifier(character.Stats.Constitution);
            int intMod = CharacterStats.GetModifier(character.Stats.Intelligence);
            int wisMod = CharacterStats.GetModifier(character.Stats.Wisdom);
            int chaMod = CharacterStats.GetModifier(character.Stats.Charisma);

            StrengthTextBlock.Text     = $"Strength:     {character.Stats.Strength} ({(strMod >= 0 ? "+" : "")}{strMod})";
            DexterityTextBlock.Text    = $"Dexterity:    {character.Stats.Dexterity} ({(dexMod >= 0 ? "+" : "")}{dexMod})";
            ConstitutionTextBlock.Text = $"Constitution: {character.Stats.Constitution} ({(conMod >= 0 ? "+" : "")}{conMod})";
            IntelligenceTextBlock.Text = $"Intelligence: {character.Stats.Intelligence} ({(intMod >= 0 ? "+" : "")}{intMod})";
            WisdomTextBlock.Text       = $"Wisdom:       {character.Stats.Wisdom} ({(wisMod >= 0 ? "+" : "")}{wisMod})";
            CharismaTextBlock.Text     = $"Charisma:     {character.Stats.Charisma} ({(chaMod >= 0 ? "+" : "")}{chaMod})";
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

        // NEW METHOD: Handles the Add Item button click
        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            var character = GameState.Instance.PlayerCharacter;
            string newItem = ItemNameTextBox.Text;

            if (character != null && !string.IsNullOrWhiteSpace(newItem))
            {
                character.Inventory.Add(newItem);
                ItemNameTextBox.Clear(); // Clear the text box after adding
                UpdateAllDisplays(); // Refresh the screen
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