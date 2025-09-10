using System;

namespace DDGameMaster.Models.Character
{
    public class CharacterStats
    {
        public string Name { get; set; } = "New Character";
        public int Level { get; set; } = 1;
        public int CurrentHP { get; set; } = 8;
        public int MaxHP { get; set; } = 8;
        public int ArmorClass { get; set; } = 10;
        public int Speed { get; set; } = 30;
        
        // Core Ability Scores
        public int Strength { get; set; } = 10;
        public int Dexterity { get; set; } = 10;
        public int Constitution { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public int Wisdom { get; set; } = 10;
        public int Charisma { get; set; } = 10;
        
        // Derived Properties
        public int StrengthModifier => CalculateModifier(Strength);
        public int DexterityModifier => CalculateModifier(Dexterity);
        public int ConstitutionModifier => CalculateModifier(Constitution);
        public int IntelligenceModifier => CalculateModifier(Intelligence);
        public int WisdomModifier => CalculateModifier(Wisdom);
        public int CharismaModifier => CalculateModifier(Charisma);
        
        public int ProficiencyBonus => (Level - 1) / 4 + 2;
        
        public event EventHandler<StatsChangedEventArgs>? StatsChanged;
        
        private int CalculateModifier(int abilityScore)
        {
            return (abilityScore - 10) / 2;
        }
        
        public void TakeDamage(int damage)
        {
            CurrentHP = Math.Max(0, CurrentHP - damage);
            OnStatsChanged();
        }
        
        public void Heal(int healing)
        {
            CurrentHP = Math.Min(MaxHP, CurrentHP + healing);
            OnStatsChanged();
        }
        
        private void OnStatsChanged()
        {
            StatsChanged?.Invoke(this, new StatsChangedEventArgs());
        }
    }
    
    public class StatsChangedEventArgs : EventArgs { }
}