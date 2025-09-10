using System.Collections.Generic; // Required for using Lists

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
        public int MaximumHitPoints { get; set; }
        public int CurrentHitPoints { get; set; }
        public int ArmorClass { get; set; }

        // NEW PROPERTY FOR INVENTORY
        public List<string> Inventory { get; set; }

        public Character()
        {
            Name = "New Character";
            Race = new CharacterRace();
            Class = new CharacterClass();
            Stats = new CharacterStats();
            Abilities = new CharacterAbilities();
            Level = 1;
            ExperiencePoints = 0;
            MaximumHitPoints = 0;
            CurrentHitPoints = 0;
            ArmorClass = 10;
            
            // When a new character is made, give them an empty inventory list
            Inventory = new List<string>();
        }
    }
}