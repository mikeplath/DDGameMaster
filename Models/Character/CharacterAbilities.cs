using System;
using System.Collections.Generic;

namespace DDGameMaster.Models.Character
{
    public class CharacterAbilities
    {
        private readonly List<Ability> _abilities = new List<Ability>();
        public const int MaxAbilities = 9;
        
        public event EventHandler<AbilitiesChangedEventArgs>? AbilitiesChanged;
        
        public IReadOnlyList<Ability> Abilities => _abilities.AsReadOnly();
        
        public bool AddAbility(Ability ability)
        {
            if (_abilities.Count < MaxAbilities)
            {
                _abilities.Add(ability);
                OnAbilitiesChanged();
                return true;
            }
            return false;
        }
        
        public void UseAbility(Ability ability)
        {
            ability.Use();
            OnAbilitiesChanged();
        }
        
        private void OnAbilitiesChanged()
        {
            AbilitiesChanged?.Invoke(this, new AbilitiesChangedEventArgs());
        }
    }
    
    public class Ability
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public AbilityType Type { get; set; }
        public int Uses { get; set; } = -1; // -1 means unlimited
        public int MaxUses { get; set; } = -1;
        public bool RequiresRest { get; set; }
        
        public bool CanUse => Uses != 0;
        
        public void Use()
        {
            if (Uses > 0)
            {
                Uses--;
            }
        }
        
        public void RestoreUses()
        {
            Uses = MaxUses;
        }
        
        public override string ToString()
        {
            if (Uses == -1) return Name;
            return $"{Name} ({Uses}/{MaxUses})";
        }
    }
    
    public enum AbilityType
    {
        ClassFeature,
        RacialTrait,
        Feat,
        Background,
        Magic
    }
    
    public class AbilitiesChangedEventArgs : EventArgs { }
}