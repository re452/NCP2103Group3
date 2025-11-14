using Microsoft.EntityFrameworkCore;
using PawGress.Data;
using PawGress.Models;

namespace PawGress.Services
{
    public class PetService
    {
        private readonly AppDbContext _db;

        public PetService(AppDbContext db)
        {
            _db = db;
        }

        // Get all pets
        public async Task<List<Pet>> GetAllPetsAsync()
        {
            return await _db.Pets.ToListAsync();
        }

        // Get user's pets, automatically assign starter pets if none exist
        public async Task<List<Pet>> GetUserPetsAsync(int userId)
        {
            var userPets = await _db.UserPets
                .Where(up => up.UserId == userId)
                .Include(up => up.Pet)
                .Select(up => up.Pet)
                .ToListAsync();

            if (!userPets.Any())
            {
                // Automatically assign first 4 pets as starter pets
                var starterPets = await _db.Pets.Take(4).ToListAsync();
                foreach (var pet in starterPets)
                {
                    _db.UserPets.Add(new UserPet
                    {
                        UserId = userId,
                        PetId = pet.Id,
                        Level = 1,
                        XP = 0
                    });
                }
                await _db.SaveChangesAsync();

                // Reload the user's pets after assignment
                userPets = await _db.UserPets
                    .Where(up => up.UserId == userId)
                    .Include(up => up.Pet)
                    .Select(up => up.Pet)
                    .ToListAsync();
            }

            return userPets;
        }

        // Add XP to a user's pet
        public async Task<bool> AddXPAsync(int userPetId, int xp)
        {
            var userPet = await _db.UserPets.Include(up => up.Pet).FirstOrDefaultAsync(up => up.Id == userPetId);
            if (userPet == null) return false;

            userPet.XP += xp;

            while (userPet.XP >= XPForNextLevel(userPet.Level))
            {
                userPet.XP -= XPForNextLevel(userPet.Level);
                userPet.Level++;
            }

            await _db.SaveChangesAsync();
            return true;
        }

        private int XPForNextLevel(int level)
        {
            return level * 100; // Simple leveling formula
        }

        // Get pet by ID
        public async Task<Pet?> GetPetByIdAsync(int petId)
        {
            return await _db.Pets.FindAsync(petId);
        }
    }
}
