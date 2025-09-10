namespace DDGameMaster.Models.Character
{
    public class CharacterClass
    {
        // The name of the character's class (e.g., "Fighter", "Wizard")
        public string Name { get; set; }

        // We can add more class-specific properties here later,
        // such as class features, hit die, etc.

        // Constructor to set a default value
        public CharacterClass()
        {
            Name = "Unknown";
        }
    }
}