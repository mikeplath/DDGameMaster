using System.Collections.Generic;
using DDGameMaster.Models.Character; // This line is needed now

namespace DDGameMaster.Models.Character
{
    public class Character
    {
        public string Name { get; set; }
        public CharacterRace Race { get; set; }
        public CharacterClass Class { get; set; }
        public CharacterStats Stats { get; set; }
        public int Level { get; set; }
        public int ExperiencePoints { get; set; }
        public int MaximumHitPoints { get; set; }
        public int CurrentHitPoints { get; set; }
        public int ArmorClass { get; set; }
        public List<Item> Inventory { get; set; }
        public List<string> SavingThrowProficiencies { get; set; }
        public List<string> SkillProficiencies { get; set; }
        public List<string> Abilities { get; set; }
        public string Appearance { get; set; }
        public string Backstory { get; set; }
        public string Alignment { get; set; }

        // NEW PROPERTIES FOR PERSONALITY
        public string PersonalityTraits { get; set; }
        public string Ideals { get; set; }
        public string Bonds { get; set; }
        public string Flaws { get; set; }

        public Character()
        {
            Name = "New Character";
            Race = new CharacterRace();
            Class = new CharacterClass();
            Stats = new CharacterStats();
            Level = 1;
            ExperiencePoints = 0;
            MaximumHitPoints = 0;
            CurrentHitPoints = 0;
            ArmorClass = 10;
            Inventory = new List<Item>();
            SavingThrowProficiencies = new List<string>();
            SkillProficiencies = new List<string>();
            Abilities = new List<string>();
            Appearance = string.Empty;
            Backstory = string.Empty;
            Alignment = "True Neutral";
            
            // Initialize the new properties
            PersonalityTraits = string.Empty;
            Ideals = string.Empty;
            Bonds = string.Empty;
            Flaws = string.Empty;
        }
    }
}