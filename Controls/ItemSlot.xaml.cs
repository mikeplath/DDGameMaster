using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DDGameMaster.Models.Equipment;

namespace DDGameMaster.Controls
{
    public partial class ItemSlot : UserControl
    {
        public static readonly DependencyProperty ItemProperty =
            DependencyProperty.Register("Item", typeof(Item), typeof(ItemSlot), 
                new PropertyMetadata(null, OnItemChanged));

        public Item? Item
        {
            get { return (Item?)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        public ItemSlot()
        {
            InitializeComponent();
            AllowDrop = true;
            MouseDoubleClick += ItemSlot_MouseDoubleClick;
        }

        private static void OnItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var slot = (ItemSlot)d;
            slot.UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            if (Item == null)
            {
                ItemText.Text = "";
                SlotBorder.Background = new SolidColorBrush(Color.FromRgb(45, 45, 48));
            }
            else
            {
                ItemText.Text = Item.ToString();
                SlotBorder.Background = GetItemColorBrush(Item.Type);
            }
        }

        private Brush GetItemColorBrush(ItemType itemType)
        {
            return itemType switch
            {
                ItemType.Weapon => new SolidColorBrush(Color.FromRgb(139, 69, 19)),
                ItemType.Armor => new SolidColorBrush(Color.FromRgb(105, 105, 105)),
                ItemType.Consumable => new SolidColorBrush(Color.FromRgb(34, 139, 34)),
                ItemType.Tool => new SolidColorBrush(Color.FromRgb(70, 130, 180)),
                ItemType.QuestItem => new SolidColorBrush(Color.FromRgb(255, 215, 0)),
                _ => new SolidColorBrush(Color.FromRgb(45, 45, 48))
            };
        }

        private void ItemSlot_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Item != null)
            {
                // Trigger item use event
                ItemUsed?.Invoke(this, Item);
            }
        }

        public event System.Action<ItemSlot, Item>? ItemUsed;
    }
}