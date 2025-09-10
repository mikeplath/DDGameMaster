using System;
using System.Collections.Generic;

namespace DDGameMaster.Models.Game
{
    public class DiceRoller
    {
        private static readonly Random _random = new Random();
        
        public static DiceResult RollD4() => RollDice(4, 1);
        public static DiceResult RollD6() => RollDice(6, 1);
        public static DiceResult RollD8() => RollDice(8, 1);
        public static DiceResult RollD10() => RollDice(10, 1);
        public static DiceResult RollD12() => RollDice(12, 1);
        public static DiceResult RollD20() => RollDice(20, 1);
        
        public static DiceResult RollDice(int sides, int count = 1, int modifier = 0)
        {
            var rolls = new List<int>();
            int total = 0;
            
            for (int i = 0; i < count; i++)
            {
                int roll = _random.Next(1, sides + 1);
                rolls.Add(roll);
                total += roll;
            }
            
            total += modifier;
            
            return new DiceResult
            {
                Sides = sides,
                Count = count,
                Modifier = modifier,
                IndividualRolls = rolls,
                Total = total,
                RollString = FormatRollString(sides, count, modifier, rolls, total)
            };
        }
        
        public static DiceResult RollWithAdvantage(int sides)
        {
            var roll1 = _random.Next(1, sides + 1);
            var roll2 = _random.Next(1, sides + 1);
            var higher = Math.Max(roll1, roll2);
            
            return new DiceResult
            {
                Sides = sides,
                Count = 2,
                Modifier = 0,
                IndividualRolls = new List<int> { roll1, roll2 },
                Total = higher,
                RollString = $"Advantage d{sides}: [{roll1}, {roll2}] = {higher}"
            };
        }
        
        public static DiceResult RollWithDisadvantage(int sides)
        {
            var roll1 = _random.Next(1, sides + 1);
            var roll2 = _random.Next(1, sides + 1);
            var lower = Math.Min(roll1, roll2);
            
            return new DiceResult
            {
                Sides = sides,
                Count = 2,
                Modifier = 0,
                IndividualRolls = new List<int> { roll1, roll2 },
                Total = lower,
                RollString = $"Disadvantage d{sides}: [{roll1}, {roll2}] = {lower}"
            };
        }
        
        private static string FormatRollString(int sides, int count, int modifier, List<int> rolls, int total)
        {
            string rollsText = string.Join(", ", rolls);
            string diceText = count == 1 ? $"d{sides}" : $"{count}d{sides}";
            string modText = modifier != 0 ? $" + {modifier}" : "";
            
            if (count == 1 && modifier == 0)
                return $"{diceText}: {total}";
            else
                return $"{diceText}{modText}: [{rollsText}] = {total}";
        }
    }
    
    public class DiceResult
    {
        public int Sides { get; set; }
        public int Count { get; set; }
        public int Modifier { get; set; }
        public List<int> IndividualRolls { get; set; } = new List<int>();
        public int Total { get; set; }
        public string RollString { get; set; } = "";
    }
}