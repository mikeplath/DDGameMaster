namespace DDGameMaster.Models.Character
{
    public class CharacterRace
    {
        // The name of the character's race (e.g., "Human", "Elf")
        public string Name { get; set; }

        // We can add more race-specific properties here later,
        // such as racial traits, speed adjustments, etc.

        // Constructor to set a default value
        public CharacterRace()
        {
            Name = "Unknown";
        }
    }
}