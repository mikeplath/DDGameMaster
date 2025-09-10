using System.Collections.Generic;

namespace DDGameMaster.Models.Character
{
    public class CharacterRace
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public Dictionary<string, int> AbilityScoreIncrease { get; set; } = new();
        public int Size { get; set; } = 30; // Speed
        public List<string> Traits { get; set; } = new();
        public List<string> Languages { get; set; } = new();
        public List<string> Proficiencies { get; set; } = new();

        public static List<CharacterRace> GetDefaultRaces()
        {
            return new List<CharacterRace>
            {
                new CharacterRace
                {
                    Name = "Human",
                    Description = "Versatile and ambitious, humans are the most adaptable of all the common races.",
                    AbilityScoreIncrease = new Dictionary<string, int> { { "ALL", 1 } },
                    Size = 30,
                    Traits = new List<string> { "Extra Skill", "Extra Feat" },
                    Languages = new List<string> { "Common", "One Extra" }
                },
                new CharacterRace
                {
                    Name = "Elf",
                    Description = "Elves are a magical people of otherworldly grace, living in places of ethereal beauty.",
                    AbilityScoreIncrease = new Dictionary<string, int> { { "Dexterity", 2 } },
                    Size = 30,
                    Traits = new List<string> { "Darkvision", "Keen Senses", "Fey Ancestry", "Trance" },
                    Languages = new List<string> { "Common", "Elvish" },
                    Proficiencies = new List<string> { "Perception" }
                },
                new CharacterRace
                {
                    Name = "Dwarf",
                    Description = "Bold and hardy, dwarves are known as skilled warriors, miners, and workers of stone and metal.",
                    AbilityScoreIncrease = new Dictionary<string, int> { { "Constitution", 2 } },
                    Size = 25,
                    Traits = new List<string> { "Darkvision", "Dwarven Resilience", "Stonecunning" },
                    Languages = new List<string> { "Common", "Dwarvish" },
                    Proficiencies = new List<string> { "Battleaxe", "Handaxe", "Light Hammer", "Warhammer" }
                },
                new CharacterRace
                {
                    Name = "Halfling",
                    Description = "The diminutive halflings survive in a world full of larger creatures by avoiding notice or, barring that, avoiding offense.",
                    AbilityScoreIncrease = new Dictionary<string, int> { { "Dexterity", 2 } },
                    Size = 25,
                    Traits = new List<string> { "Lucky", "Brave", "Halfling Nimbleness" },
                    Languages = new List<string> { "Common", "Halfling" }
                }
            };
        }
    }
}