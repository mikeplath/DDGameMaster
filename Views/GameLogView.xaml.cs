using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DDGameMaster.Models.Game; // Use our GameState

namespace DDGameMaster.Views
{
    public partial class GameLogView : Page
    {
        public GameLogView()
        {
            InitializeComponent();
            // When the page loads, put the saved log text into the textbox
            LogTextBox.Text = GameState.Instance.GameLog;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            // When we leave, save the current text from the textbox into our GameState
            GameState.Instance.GameLog = LogTextBox.Text;

            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}