using System;

namespace DDGameMaster.Models.Character
{
    public class CharacterStats
    {
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }

        // NEW METHOD
        // This calculates the modifier for a given stat score.
        // The formula is (Score - 10) / 2, rounded down.
        public static int GetModifier(int score)
        {
            return (int)Math.Floor((score - 10) / 2.0);
        }

        public CharacterStats()
        {
            // Default stats to 10, which has a +0 modifier
            Strength = 10;
            Dexterity = 10;
            Constitution = 10;
            Intelligence = 10;
            Wisdom = 10;
            Charisma = 10;
        }
    }
}