namespace DDGameMaster.Models.Character
{
    public class CharacterStats
    {
        // Core character statistics
        public int Level { get; set; }
        public int ExperiencePoints { get; set; }
        public int MaxHitPoints { get; set; }
        public int CurrentHitPoints { get; set; }
        public int ArmorClass { get; set; }
        public int Speed { get; set; }

        // Constructor to set default values
        public CharacterStats()
        {
            Level = 1;
            ExperiencePoints = 0;
            MaxHitPoints = 10;
            CurrentHitPoints = 10;
            ArmorClass = 10;
            Speed = 30; // Standard speed in feet for most races
        }
    }
}