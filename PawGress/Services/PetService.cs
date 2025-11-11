using Microsoft.EntityFrameworkCore;
using PawGress.Data;
using PawGress.Models;

namespace PawGress.Services
{
    public class PetService
    {
        private readonly AppDbContext _context;

        public PetService(AppDbContext context)
        {
            _context = context;
        }

        // Get all pets
        public async Task<List<Pet>> GetAllPetsAsync()
        {
            return await _context.Pets.ToListAsync();
        }

        // Get pet by ID
        public async Task<Pet?> GetPetByIdAsync(int id)
        {
            return await _context.Pets.FindAsync(id);
        }

        // Update XP and level
        public async Task<bool> AddXPAsync(int petId, int xp)
        {
            var pet = await _context.Pets.FindAsync(petId);
            if (pet == null) return false;

            pet.XP += xp;

            while (pet.XP >= XPForNextLevel(pet.Level))
            {
                pet.XP -= XPForNextLevel(pet.Level);
                pet.Level++;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        private int XPForNextLevel(int currentLevel)
        {
            return currentLevel * 100; // simple example
        }

        // Update other pet stats if needed (like health)
        public async Task<bool> UpdatePetAsync(Pet pet)
        {
            var existing = await _context.Pets.FindAsync(pet.Id);
            if (existing == null) return false;

            existing.XP = pet.XP;
            existing.Level = pet.Level;
            existing.Health = pet.Health; // if you add Health later

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<Pet>> GetUserPetsAsync(int userId)
        {
            return await _context.UserPets
                .Where(up => up.UserId == userId)
                .Include(up => up.Pet)
                .Select(up => up.Pet)
                .ToListAsync();
        }

        // Assign a starter pet to a user (max 4)
        public async Task<bool> AssignStarterPetToUserAsync(int userId, int petId)
        {
            var existingCount = await _context.UserPets.CountAsync(up => up.UserId == userId);
            if (existingCount >= 4)
                return false; // user already has 4 pets

            _context.UserPets.Add(new UserPet
            {
                UserId = userId,
                PetId = petId
            });
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
