namespace DDGameMaster.Models.Character
{
    public class Character
    {
        public string Name { get; set; }
        public CharacterRace Race { get; set; }
        public CharacterClass Class { get; set; }
        public CharacterStats Stats { get; set; }
        public CharacterAbilities Abilities { get; set; }
        public int Level { get; set; }
        public int ExperiencePoints { get; set; }

        // NEW PROPERTIES FOR HEALTH
        public int MaximumHitPoints { get; set; }
        public int CurrentHitPoints { get; set; }

        public Character()
        {
            Name = "New Character";
            Race = new CharacterRace();
            Class = new CharacterClass();
            Stats = new CharacterStats();
            Abilities = new CharacterAbilities();
            Level = 1;
            ExperiencePoints = 0;
            MaximumHitPoints = 0; // Will be calculated on creation
            CurrentHitPoints = 0; // Will be calculated on creation
        }
    }
}