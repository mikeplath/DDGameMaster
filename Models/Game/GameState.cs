using DDGameMaster.Models.Character; // This allows us to use the Character blueprint we made

namespace DDGameMaster.Models.Game
{
    public class GameState
    {
        // This is a special, static "instance" of our GameState.
        // "static" means there will only ever be ONE of these in the entire program.
        // This allows any part of the program to access the same GameState.
        private static GameState _instance;
        public static GameState Instance
        {
            get
            {
                // If nobody has asked for the GameState yet, create it.
                if (_instance == null)
                {
                    _instance = new GameState();
                }
                return _instance;
            }
        }

        // This is where we will store the character after it is created.
        public Character PlayerCharacter { get; set; }

        // The constructor is private to ensure nobody else can create a new GameState.
        // They must always use the "Instance" property above.
        private GameState()
        {
        }
    }
}