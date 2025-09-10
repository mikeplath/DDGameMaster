using System.Collections.Generic;

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
        public List<string> Inventory { get; set; }
        public List<string> SavingThrowProficiencies { get; set; }

        // NEW PROPERTY FOR SKILL PROFICIENCIES
        public List<string> SkillProficiencies { get; set; }

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
            Inventory = new List<string>();
            SavingThrowProficiencies = new List<string>();
            
            // When a new character is made, give them an empty list for skill proficiencies
            SkillProficiencies = new List<string>();
        }
    }
}