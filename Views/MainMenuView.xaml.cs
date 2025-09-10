using System;
using System.Windows;
using System.Windows.Controls;
using DDGameMaster.Services; // Import our new services folder

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
            this.NavigationService.Navigate(new Uri("Views/CharacterCreationView.xaml", UriKind.Relative));
        }

        private void ViewCharacterSheet_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Views/CharacterSheetView.xaml", UriKind.Relative));
        }

        private void GameLog_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Views/GameLogView.xaml", UriKind.Relative));
        }

        // ** NEW METHOD FOR SAVING **
        private void SaveGame_Click(object sender, RoutedEventArgs e)
        {
            SaveLoadService.SaveState();
        }

        // ** NEW METHOD FOR LOADING **
        private void LoadGame_Click(object sender, RoutedEventArgs e)
        {
            SaveLoadService.LoadState();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}