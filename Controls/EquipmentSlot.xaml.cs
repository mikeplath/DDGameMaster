using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DDGameMaster.Models.Equipment;

namespace DDGameMaster.Controls
{
    public partial class EquipmentSlot : UserControl
    {
        public static readonly DependencyProperty SlotTypeProperty =
            DependencyProperty.Register("SlotType", typeof(Models.Equipment.EquipmentSlot), typeof(EquipmentSlot), 
                new PropertyMetadata(Models.Equipment.EquipmentSlot.Head, OnSlotTypeChanged));

        public static readonly DependencyProperty EquippedItemProperty =
            DependencyProperty.Register("EquippedItem", typeof(Item), typeof(EquipmentSlot), 
                new PropertyMetadata(null, OnEquippedItemChanged));

        public Models.Equipment.EquipmentSlot SlotType
        {
            get { return (Models.Equipment.EquipmentSlot)GetValue(SlotTypeProperty); }
            set { SetValue(SlotTypeProperty, value); }
        }

        public Item? EquippedItem
        {
            get { return (Item?)GetValue(EquippedItemProperty); }
            set { SetValue(EquippedItemProperty, value); }
        }

        public EquipmentSlot()
        {
            InitializeComponent();
            AllowDrop = true;
            MouseDoubleClick += EquipmentSlot_MouseDoubleClick;
        }

        private static void OnSlotTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var slot = (EquipmentSlot)d;
            slot.UpdateSlotDisplay();
        }

        private static void OnEquippedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var slot = (EquipmentSlot)d;
            slot.UpdateSlotDisplay();
        }

        private void UpdateSlotDisplay()
        {
            if (EquippedItem == null)
            {
                SlotLabel.Text = GetSlotLabel(SlotType);
                SlotBorder.Background = new SolidColorBrush(Color.FromRgb(45, 45, 48));
            }
            else
            {
                SlotLabel.Text = "";
                SlotBorder.Background = GetItemColorBrush(EquippedItem.Type);
                ToolTip = $"{EquippedItem.Name}\n{EquippedItem.Description}";
            }
        }

        private string GetSlotLabel(Models.Equipment.EquipmentSlot slotType)
        {
            return slotType switch
            {
                Models.Equipment.EquipmentSlot.Head => "HEAD",
                Models.Equipment.EquipmentSlot.Chest => "CHEST",
                Models.Equipment.EquipmentSlot.Feet => "FEET",
                Models.Equipment.EquipmentSlot.MainHand => "MAIN",
                Models.Equipment.EquipmentSlot.OffHand => "OFF",
                _ => ""
            };
        }

        private Brush GetItemColorBrush(ItemType itemType)
        {
            return itemType switch
            {
                ItemType.Weapon => new SolidColorBrush(Color.FromRgb(139, 69, 19)),
                ItemType.Armor => new SolidColorBrush(Color.FromRgb(105, 105, 105)),
                ItemType.Shield => new SolidColorBrush(Color.FromRgb(70, 130, 180)),
                _ => new SolidColorBrush(Color.FromRgb(45, 45, 48))
            };
        }

        private void EquipmentSlot_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (EquippedItem != null)
            {
                ItemUnequipped?.Invoke(this, EquippedItem);
            }
        }

        public event System.Action<EquipmentSlot, Item>? ItemUnequipped;
    }
}