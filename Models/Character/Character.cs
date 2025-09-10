namespace DDGameMaster.Models.Character
{
    public class Character
    {
        public string Name { get; set; }

        // These properties will hold the other parts of the character
        public CharacterRace Race { get; set; }
        public CharacterClass Class { get; set; }
        public CharacterStats Stats { get; set; }
        public CharacterAbilities Abilities { get; set; }

        // Constructor to initialize the character's parts
        public Character()
        {
            Race = new CharacterRace();
            Class = new CharacterClass();
            Stats = new CharacterStats();
            Abilities = new CharacterAbilities();
        }
    }
}