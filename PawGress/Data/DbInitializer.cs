using PawGress.Models;
using Microsoft.EntityFrameworkCore;

namespace PawGress.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            // Apply migrations safely
            context.Database.Migrate();

            // --- Seed Pets ---
            if (!context.Pets.Any())
            {
                var pets = new[]
                {
                    new Pet { Name = "Fluffy", Type = "Cat", Age = 1, Level = 1, XP = 0, Health = 100, Rarity = "Common" },
                    new Pet { Name = "Barky", Type = "Dog", Age = 1, Level = 1, XP = 0, Health = 100, Rarity = "Common" },
                    new Pet { Name = "Chirpy", Type = "Bird", Age = 1, Level = 1, XP = 0, Health = 100, Rarity = "Uncommon" },
                    new Pet { Name = "Scaly", Type = "Lizard", Age = 1, Level = 1, XP = 0, Health = 100, Rarity = "Rare" }
                };

                context.Pets.AddRange(pets);
                context.SaveChanges();
            }

            // --- Seed Tasks ---
            if (!context.Tasks.Any())
            {
                var tasks = new[]
                {
                    new TaskItem { Name = "5 Push-ups", XP = 20, Completed = false, Category = "Upper", IsHabit = false, UserId = 0 },
                    new TaskItem { Name = "10 Squats", XP = 25, Completed = false, Category = "Lower", IsHabit = false, UserId = 0 },
                    new TaskItem { Name = "Plank 30s", XP = 15, Completed = false, Category = "Core", IsHabit = false, UserId = 0 },
                    new TaskItem { Name = "Jog 5 min", XP = 30, Completed = false, Category = "Cardio", IsHabit = false, UserId = 0 }
                };

                context.Tasks.AddRange(tasks);
                context.SaveChanges();
            }

            // --- Seed Pre-made Challenges ---
            if (!context.Challenges.Any())
            {
                var allTasks = context.Tasks.ToList();

                var challenge1 = new Challenge
                {
                    Name = "Morning Routine",
                    IsPreMade = true,
                    Completed = false,
                    Tasks = allTasks.Take(2).ToList()
                };

                var challenge2 = new Challenge
                {
                    Name = "Core Blast",
                    IsPreMade = true,
                    Completed = false,
                    Tasks = allTasks.Where(t => t.Category == "Core").ToList()
                };

                context.Challenges.AddRange(challenge1, challenge2);
                context.SaveChanges();
            }
        }
    }
}
