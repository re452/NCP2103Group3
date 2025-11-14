namespace PawGress.Models
{
    public class UserPet
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int PetId { get; set; }
        public Pet Pet { get; set; } = null!;

        public int Level { get; set; } = 1;
        public int XP { get; set; } = 0;
    }
}
