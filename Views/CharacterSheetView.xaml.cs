using System; // Required for Int32.TryParse
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DDGameMaster.Models.Character;
using DDGameMaster.Models.Game;

namespace DDGameMaster.Views
{
    public partial class CharacterSheetView : Page
    {
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
                NameTextBlock.Text = character.Name;
                RaceClassLevelTextBlock.Text = $"Level {character.Level} {character.Race.Name} {character.Class.Name}";
                
                // This method will now be used to update HP display in multiple places
                UpdateHpDisplay();

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
            else
            {
                NameTextBlock.Text = "No Character Found";
                RaceClassLevelTextBlock.Text = "Please create a new character from the main menu.";
            }
        }
        
        // NEW METHOD: Updates the HP text block.
        private void UpdateHpDisplay()
        {
            var character = GameState.Instance.PlayerCharacter;
            if (character != null)
            {
                HitPointsTextBlock.Text = $"HP: {character.CurrentHitPoints} / {character.MaximumHitPoints}";
            }
        }

        // NEW METHOD: Handles the Damage button click.
        private void Damage_Click(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(HpAmountTextBox.Text, out int amount))
            {
                var character = GameState.Instance.PlayerCharacter;
                if (character != null)
                {
                    character.CurrentHitPoints -= amount;
                    if (character.CurrentHitPoints < 0)
                    {
                        character.CurrentHitPoints = 0; // HP cannot go below 0
                    }
                    UpdateHpDisplay(); // Refresh the display
                }
            }
        }

        // NEW METHOD: Handles the Heal button click.
        private void Heal_Click(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(HpAmountTextBox.Text, out int amount))
            {
                var character = GameState.Instance.PlayerCharacter;
                if (character != null)
                {
                    character.CurrentHitPoints += amount;
                    if (character.CurrentHitPoints > character.MaximumHitPoints)
                    {
                        character.CurrentHitPoints = character.MaximumHitPoints; // HP cannot go above max
                    }
                    UpdateHpDisplay(); // Refresh the display
                }
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