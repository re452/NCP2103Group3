using System.ComponentModel.DataAnnotations;

namespace PawGress.Models
{
    public class Pet
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        // Rarity of the pet: Standard, Rare, Mythical
        public string Rarity { get; set; } = "Standard";

        // Health points of the pet
        public int Health { get; set; } = 100;

        // Experience points
        public int XP { get; set; } = 0;

        // Level of the pet
        public int Level { get; set; } = 1;

        // Pet type (e.g., Cat, Dog, Bunny)
        public string Type { get; set; } = string.Empty;

        // Age of the pet
        public int Age { get; set; } = 0;
    }
}
