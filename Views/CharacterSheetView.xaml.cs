using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DDGameMaster.Models.Character; // We need this to get the static GetModifier method
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
                // Display Name, Race, Class, and Level
                NameTextBlock.Text = character.Name;
                RaceClassLevelTextBlock.Text = $"Level {character.Level} {character.Race.Name} {character.Class.Name}";

                // Get the modifier for each stat and display it
                int strMod = CharacterStats.GetModifier(character.Stats.Strength);
                int dexMod = CharacterStats.GetModifier(character.Stats.Dexterity);
                int conMod = CharacterStats.GetModifier(character.Stats.Constitution);
                int intMod = CharacterStats.GetModifier(character.Stats.Intelligence);
                int wisMod = CharacterStats.GetModifier(character.Stats.Wisdom);
                int chaMod = CharacterStats.GetModifier(character.Stats.Charisma);

                // Display the stat score and its modifier (e.g., "Strength: 14 (+2)")
                StrengthTextBlock.Text     = $"Strength:     {character.Stats.Strength} ({(strMod >= 0 ? "+" : "")}{strMod})";
                DexterityTextBlock.Text    = $"Dexterity:    {character.Stats.Dexterity} ({(dexMod >= 0 ? "+" : "")}{dexMod})";
                ConstitutionTextBlock.Text = $"Constitution: {character.Stats.Constitution} ({(conMod >= 0 ? "+" : "")}{conMod})";
                IntelligenceTextBlock.Text = $"Intelligence: {character.Stats.Intelligence} ({(intMod >= 0 ? "+" : "")}{intMod})";
                WisdomTextBlock.Text       = $"Wisdom:       {character.Stats.Wisdom} ({(wisMod >= 0 ? "+" : "")}{wisMod})";
                CharismaTextBlock.Text     = $"Charisma:     {character.Stats.Charisma} ({(chaMod >= 0 ? "+" : "")}{chaMod})";
            }
            else
            {
                // If no character exists, show a helpful message
                NameTextBlock.Text = "No Character Found";
                RaceClassLevelTextBlock.Text = "Please create a new character from the main menu.";
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