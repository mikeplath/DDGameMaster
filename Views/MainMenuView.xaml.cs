using System;
using System.Windows;
using System.Windows.Controls;

namespace DDGameMaster.Views
{
    public partial class MainMenuView : Page
    {
        public MainMenuView()
        {
            InitializeComponent();
        }

        private void CreateNewCharacter_Click(object sender, RoutedEventArgs e)
        {
            // This navigates to the character creation screen
            this.NavigationService.Navigate(new Uri("Views/CharacterCreationView.xaml", UriKind.Relative));
        }

        // **THIS IS THE NEW METHOD FOR OUR NEW BUTTON**
        private void ViewCharacterSheet_Click(object sender, RoutedEventArgs e)
        {
            // This will navigate to the new character sheet screen when the button is clicked
            this.NavigationService.Navigate(new Uri("Views/CharacterSheetView.xaml", UriKind.Relative));
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            // This closes the application
            Application.Current.Shutdown();
        }
    }
}