namespace DDGameMaster.Models.Character
{
    public class Character
    {
        public string Name { get; set; }
        public CharacterRace Race { get; set; }
        public CharacterClass Class { get; set; }
        public CharacterStats Stats { get; set; }
        public CharacterAbilities Abilities { get; set; }

        // NEW PROPERTIES
        public int Level { get; set; }
        public int ExperiencePoints { get; set; }

        public Character()
        {
            Name = "New Character";
            Race = new CharacterRace();
            Class = new CharacterClass();
            Stats = new CharacterStats();
            Abilities = new CharacterAbilities();
            Level = 1; // Characters start at level 1
            ExperiencePoints = 0; // Characters start with 0 XP
        }
    }
}