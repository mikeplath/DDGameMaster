using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DDGameMaster.Models.Magic;

namespace DDGameMaster.Controls
{
    public partial class SpellSlot : UserControl
    {
        public static readonly DependencyProperty SpellProperty =
            DependencyProperty.Register("Spell", typeof(Spell), typeof(SpellSlot), 
                new PropertyMetadata(null, OnSpellChanged));

        public Spell? Spell
        {
            get { return (Spell?)GetValue(SpellProperty); }
            set { SetValue(SpellProperty, value); }
        }

        public SpellSlot()
        {
            InitializeComponent();
            MouseDoubleClick += SpellSlot_MouseDoubleClick;
        }

        private static void OnSpellChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var slot = (SpellSlot)d;
            slot.UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            if (Spell == null)
            {
                SpellText.Text = "";
                SlotBorder.Background = new SolidColorBrush(Color.FromRgb(45, 45, 48));
                ToolTip = null;
            }
            else
            {
                SpellText.Text = Spell.ToString();
                SlotBorder.Background = GetSpellColorBrush(Spell.Level);
                ToolTip = $"{Spell.Name} (Level {Spell.Level})\n{Spell.Description}\n\nCasting Time: {Spell.CastingTime}\nRange: {Spell.Range}\nDuration: {Spell.Duration}";
            }
        }

        private Brush GetSpellColorBrush(int spellLevel)
        {
            return spellLevel switch
            {
                0 => new SolidColorBrush(Color.FromRgb(128, 128, 128)), // Cantrips - Gray
                1 => new SolidColorBrush(Color.FromRgb(70, 130, 180)),  // Level 1 - Steel Blue
                2 => new SolidColorBrush(Color.FromRgb(60, 179, 113)),  // Level 2 - Medium Sea Green
                3 => new SolidColorBrush(Color.FromRgb(255, 140, 0)),   // Level 3 - Dark Orange
                4 => new SolidColorBrush(Color.FromRgb(186, 85, 211)),  // Level 4 - Medium Orchid
                5 => new SolidColorBrush(Color.FromRgb(220, 20, 60)),   // Level 5 - Crimson
                _ => new SolidColorBrush(Color.FromRgb(255, 215, 0))    // Level 6+ - Gold
            };
        }

        private void SpellSlot_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Spell != null)
            {
                SpellCast?.Invoke(this, Spell);
            }
        }

        public event System.Action<SpellSlot, Spell>? SpellCast;
    }
}