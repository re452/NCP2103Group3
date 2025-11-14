namespace PawGress.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = "/Images/placeholder.png"; // image from wwwroot/Images
        public string Description { get; set; } = string.Empty;
        public string Rarity { get; set; } = "Standard"; // Standard, Rare, Mythical
        public int Level { get; set; } = 1;
        public int XP { get; set; } = 0;

        // Optional stats
        public int Health { get; set; } = 100;

        // Relationship
        public int? UserId { get; set; } // null = unassigned
        public User? User { get; set; }
    }
}
