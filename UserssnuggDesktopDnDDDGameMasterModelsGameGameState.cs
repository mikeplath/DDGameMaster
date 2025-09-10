using DDGameMaster.Models.Character;

namespace DDGameMaster.Models.Game
{
    public sealed class GameState
    {
        private static readonly GameState instance = new GameState();

        public Character PlayerCharacter { get; set; }
        
        // NEW PROPERTY: This will hold the text for our game log.
        public string GameLog { get; set; }

        private GameState() 
        {
            // When the game starts, the log is empty.
            GameLog = string.Empty;
        }

        public static GameState Instance
        {
            get { return instance; }
        }
    }
}