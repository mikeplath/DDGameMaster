namespace DDGameMaster.Models.Character
{
    public class CharacterClass
    {
        public string Name { get; set; }
        
        // NEW PROPERTY: This represents the die used for health (e.g., 10 for a d10)
        public int HitDie { get; set; }

        public CharacterClass()
        {
            Name = "Unknown";
            HitDie = 8; // Default to a d8
        }
    }
}