using System;
using System.Collections.Generic;
using System.Linq;

namespace DDGameMaster.Models.Magic
{
    public class SpellManager
    {
        private readonly List<Spell> _knownSpells = new List<Spell>();
        private readonly List<Spell> _preparedSpells = new List<Spell>();
        public const int MaxPreparedSpells = 9;
        
        public event EventHandler<SpellsChangedEventArgs>? SpellsChanged;
        
        public IReadOnlyList<Spell> KnownSpells => _knownSpells.AsReadOnly();
        public IReadOnlyList<Spell> PreparedSpells => _preparedSpells.Where(s => s.IsPrepared).ToList().AsReadOnly();
        
        public bool LearnSpell(Spell spell)
        {
            if (!_knownSpells.Any(s => s.Name == spell.Name))
            {
                _knownSpells.Add(spell);
                OnSpellsChanged();
                return true;
            }
            return false;
        }
        
        public bool PrepareSpell(Spell spell)
        {
            if (_knownSpells.Contains(spell) && _preparedSpells.Count < MaxPreparedSpells)
            {
                spell.IsPrepared = true;
                if (!_preparedSpells.Contains(spell))
                {
                    _preparedSpells.Add(spell);
                }
                OnSpellsChanged();
                return true;
            }
            return false;
        }
        
        public void UnprepareSpell(Spell spell)
        {
            spell.IsPrepared = false;
            _preparedSpells.Remove(spell);
            OnSpellsChanged();
        }
        
        public void CastSpell(Spell spell)
        {
            // TODO: Implement spell slot consumption
            OnSpellsChanged();
        }
        
        private void OnSpellsChanged()
        {
            SpellsChanged?.Invoke(this, new SpellsChangedEventArgs());
        }
    }
    
    public class SpellsChangedEventArgs : EventArgs { }
}