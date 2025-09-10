using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Win32; // Required for the Save/Open dialog windows
using System.IO; // Required for reading and writing files

namespace DDGameMaster.Views
{
    public partial class NotesView : Page
    {
        public NotesView()
        {
            InitializeComponent();
        }

        // NEW METHOD FOR THE "SAVE NOTES" BUTTON
        private void SaveNotes_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.Title = "Save Your Notes";
            saveFileDialog.FileName = "DM_Notes.txt";

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    File.WriteAllText(saveFileDialog.FileName, NotesTextBox.Text);
                    MessageBox.Show("Notes saved successfully!");
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"An error occurred while saving: {ex.Message}");
                }
            }
        }

        // NEW METHOD FOR THE "LOAD NOTES" BUTTON
        private void LoadNotes_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text File (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.Title = "Load Your Notes";

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    NotesTextBox.Text = File.ReadAllText(openFileDialog.FileName);
                    MessageBox.Show("Notes loaded successfully!");
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"An error occurred while loading: {ex.Message}");
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