using Microsoft.EntityFrameworkCore;
using PawGress.Models;

namespace PawGress.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Pet> Pets { get; set; } = default!;
        public DbSet<TaskItem> Tasks { get; set; } = default!;
        public DbSet<Challenge> Challenges { get; set; } = default!;
        public DbSet<UserTask> UserTasks { get; set; } = default!;
        public DbSet<UserChallenge> UserChallenges { get; set; } = default!;
        public DbSet<UserPet> UserPets { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Pets
            modelBuilder.Entity<Pet>().HasData(
                new Pet { Id = 1, Name = "Fluffy", Rarity = "Standard", XP = 0 },
                new Pet { Id = 2, Name = "Sparky", Rarity = "Standard", XP = 0 },
                new Pet { Id = 3, Name = "Shadow", Rarity = "Rare", XP = 0 },
                new Pet { Id = 4, Name = "Aurora", Rarity = "Mythical", XP = 0 }
            );

            // Seed Users (store hashed password for the built-in test user)
            // Use SHA256 hashing consistent with UserService
            var pwdBytes = System.Text.Encoding.UTF8.GetBytes("password");
            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                var hashed = Convert.ToBase64String(sha.ComputeHash(pwdBytes));
                modelBuilder.Entity<User>().HasData(
                    new User { Id = 1, Username = "testuser", Password = hashed }
                );
            }

            // Seed Tasks (normal tasks)
            modelBuilder.Entity<TaskItem>().HasData(
                new TaskItem { Id = 1, Name = "10 Push-ups", XP = 10, Category = "Upper", IsHabit = false, UserId = 0 },
                new TaskItem { Id = 2, Name = "20 Sit-ups", XP = 15, Category = "Core", IsHabit = false, UserId = 0 },
                new TaskItem { Id = 3, Name = "15 Squats", XP = 10, Category = "Lower", IsHabit = false, UserId = 0 }
            );

            // Seed Habit Tasks
            modelBuilder.Entity<TaskItem>().HasData(
                new TaskItem { Id = 4, Name = "Drink Water", XP = 5, Category = "Core", IsHabit = true, UserId = 0 },
                new TaskItem { Id = 5, Name = "Stretch", XP = 5, Category = "Upper", IsHabit = true, UserId = 0 }
            );

            // Seed Challenges
            modelBuilder.Entity<Challenge>().HasData(
                new Challenge { Id = 1, Name = "Starter Challenge", IsPreMade = true, Completed = false }
            );
        }
    }
}
