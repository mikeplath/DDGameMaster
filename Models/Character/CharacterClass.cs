using System.Collections.Generic;

namespace DDGameMaster.Models.Character
{
    public class CharacterClass
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int HitDie { get; set; } = 8;
        public List<string> PrimaryAbilities { get; set; } = new();
        public List<string> SavingThrowProficiencies { get; set; } = new();
        public List<string> SkillProficiencies { get; set; } = new();
        public List<string> StartingEquipment { get; set; } = new();
        public bool Spellcaster { get; set; } = false;

        public static List<CharacterClass> GetDefaultClasses()
        {
            return new List<CharacterClass>
            {
                new CharacterClass
                {
                    Name = "Fighter",
                    Description = "A master of martial combat, skilled with a variety of weapons and armor.",
                    HitDie = 10,
                    PrimaryAbilities = new List<string> { "Strength", "Dexterity" },
                    SavingThrowProficiencies = new List<string> { "Strength", "Constitution" },
                    SkillProficiencies = new List<string> { "Acrobatics", "Animal Handling", "Athletics", "History", "Insight", "Intimidation", "Perception", "Survival" },
                    StartingEquipment = new List<string> { "Chain mail", "Shield", "Longsword", "Two handaxes", "Light crossbow and 20 bolts" },
                    Spellcaster = false
                },
                new CharacterClass
                {
                    Name = "Wizard",
                    Description = "A scholarly magic-user capable of manipulating the structures of spells to suit their needs.",
                    HitDie = 6,
                    PrimaryAbilities = new List<string> { "Intelligence" },
                    SavingThrowProficiencies = new List<string> { "Intelligence", "Wisdom" },
                    SkillProficiencies = new List<string> { "Arcana", "History", "Insight", "Investigation", "Medicine", "Religion" },
                    StartingEquipment = new List<string> { "Quarterstaff", "Dagger", "Spellbook", "Scholar's pack" },
                    Spellcaster = true
                },
                new CharacterClass
                {
                    Name = "Rogue",
                    Description = "A scoundrel who uses stealth and trickery to accomplish what others do by brute force.",
                    HitDie = 8,
                    PrimaryAbilities = new List<string> { "Dexterity" },
                    SavingThrowProficiencies = new List<string> { "Dexterity", "Intelligence" },
                    SkillProficiencies = new List<string> { "Acrobatics", "Athletics", "Deception", "Insight", "Intimidation", "Investigation", "Perception", "Performance", "Persuasion", "Sleight of Hand", "Stealth" },
                    StartingEquipment = new List<string> { "Leather armor", "Two shortswords", "Thieves' tools", "Burglar's pack" },
                    Spellcaster = false
                },
                new CharacterClass
                {
                    Name = "Cleric",
                    Description = "A priestly champion who wields divine magic in service of a higher power.",
                    HitDie = 8,
                    PrimaryAbilities = new List<string> { "Wisdom" },
                    SavingThrowProficiencies = new List<string> { "Wisdom", "Charisma" },
                    SkillProficiencies = new List<string> { "History", "Insight", "Medicine", "Persuasion", "Religion" },
                    StartingEquipment = new List<string> { "Scale mail", "Shield", "Mace", "Light crossbow and 20 bolts", "Priest's pack" },
                    Spellcaster = true
                }
            };
        }
    }
}