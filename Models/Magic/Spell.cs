using System;

namespace DDGameMaster.Models.Magic
{
    public class Spell
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int Level { get; set; }
        public SpellSchool School { get; set; }
        public string CastingTime { get; set; } = "";
        public string Range { get; set; } = "";
        public string Components { get; set; } = "";
        public string Duration { get; set; } = "";
        public string Damage { get; set; } = "";
        public bool IsConcentration { get; set; }
        public bool IsRitual { get; set; }
        public bool IsPrepared { get; set; }
        
        public override string ToString()
        {
            return $"{Name} (L{Level})";
        }
    }
    
    public enum SpellSchool
    {
        Abjuration,
        Conjuration,
        Divination,
        Enchantment,
        Evocation,
        Illusion,
        Necromancy,
        Transmutation
    }
}