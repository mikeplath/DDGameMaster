namespace DDGameMaster.Models.Character
{
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Item()
        {
            Name = "New Item";
            Description = string.Empty;
        }
    }
}