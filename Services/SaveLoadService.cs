using System.IO;
using System.Text.Json;
using System.Windows;
using DDGameMaster.Models.Game; // We need this to access GameState

namespace DDGameMaster.Services
{
    public static class SaveLoadService
    {
        // This is the name of our save file.
        private static readonly string _saveFilePath = Path.Combine(Directory.GetCurrentDirectory(), "savegame.json");

        public static void SaveState()
        {
            try
            {
                // Get the current game state
                var gameState = GameState.Instance;
                
                // Set up the serializer to make the JSON file easy to read
                var options = new JsonSerializerOptions { WriteIndented = true };
                
                // Convert the entire game state object into a JSON string
                string jsonString = JsonSerializer.Serialize(gameState, options);
                
                // Write the string to the file
                File.WriteAllText(_saveFilePath, jsonString);
                
                MessageBox.Show("Game Saved Successfully!");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error saving game: {ex.Message}");
            }
        }

        public static void LoadState()
        {
            try
            {
                // Check if the save file actually exists
                if (File.Exists(_saveFilePath))
                {
                    // Read all the text from the file
                    string jsonString = File.ReadAllText(_saveFilePath);
                    
                    // Convert the JSON string back into a GameState object
                    var loadedState = JsonSerializer.Deserialize<GameState>(jsonString);

                    // If the loaded data is valid, update our current game with it
                    if (loadedState != null)
                    {
                        GameState.Instance.UpdateState(loadedState);
                        MessageBox.Show("Game Loaded Successfully!");
                    }
                }
                else
                {
                    MessageBox.Show("No save file found.");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error loading game: {ex.Message}");
            }
        }
    }
}