// Services/ChallengeService.cs
using PawGress.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PawGress.Services
{
    public class ChallengeService
    {
        private readonly TaskService _taskService;

        public ChallengeService(TaskService taskService)
        {
            _taskService = taskService;
        }

        private static TaskItem CreateTask(string name, string description, string category) =>
            new TaskItem { Name = name, Description = description, Category = category, UserName = "System" };

        private readonly List<Challenge> _challenges = new List<Challenge>
        {
            new Challenge 
            { 
                Id = 1, 
                Name = "Diamond Push-Ups Challenge", 
                ImagePath = "/Images/challenge_pushup.png", 
                Category = "UPPER BODY", 
                Difficulty = "HARD", 
                MainMuscle = "CHEST", 
                SecondaryMuscles = "TRICEPS, SHOULDERS",
                Tasks = new List<TaskItem> { CreateTask("3 Sets of 10 Diamond Push-Ups", "Focus on form and slow negatives.", "Endurance") }
            },
            new Challenge 
            { 
                Id = 2, 
                Name = "Bulgarian Split Squat Challenge", 
                ImagePath = "/Images/challenge_squat.png", 
                Category = "LOWER BODY", 
                Difficulty = "MEDIUM", 
                MainMuscle = "QUADS", 
                SecondaryMuscles = "GLUTES, CORE",
                Tasks = new List<TaskItem> { CreateTask("3 Sets of 8/leg Bulgarian Split Squat", "Hold a dumbbell for extra challenge.", "Strength") }
            },
            new Challenge 
            { 
                Id = 3, 
                Name = "Lateral Raise Challenge", 
                ImagePath = "/Images/challenge_lateralraise.png", 
                Category = "UPPER BODY", 
                Difficulty = "HARD", 
                MainMuscle = "BACK", 
                SecondaryMuscles = "BICEPS, CORE, SHOULDERS",
                Tasks = new List<TaskItem> { CreateTask("5 Sets of 12 Lateral Raise", "Superset with Bicep Curls.", "Volume") }
            },
            new Challenge 
            { 
                Id = 4, 
                Name = "Weighted Squats Challenge", 
                ImagePath = "/Images/challenge_weighted_squat.png", 
                Category = "LOWER BODY", 
                Difficulty = "EASY", 
                MainMuscle = "LEGS", 
                SecondaryMuscles = "GLUTES, CORE, HAMSTRING",
                Tasks = new List<TaskItem> { CreateTask("3 Sets of 15 Bodyweight Squats", "Maintain proper depth and chest up.", "Warmup") }
            },
            new Challenge 
            { 
                Id = 7, 
                Name = "30-Minute Run Challenge", 
                ImagePath = "/Images/challenge_running.png", 
                Category = "CARDIO", 
                Difficulty = "MEDIUM", 
                MainMuscle = "LEGS", 
                SecondaryMuscles = "LUNGS, CALVES",
                Tasks = new List<TaskItem> { CreateTask("Jog for 30 minutes straight", "Keep a steady, maintainable pace.", "Cardio") }
            },
        }; // <-- This is the crucial closing brace and semicolon for the list initialization.

        public Task<List<Challenge>> GetAllChallengesAsync() => Task.FromResult(_challenges);
        
        public async Task AddChallengeToTasksAsync(int challengeId, string userName)
        {
            var challenge = _challenges.FirstOrDefault(c => c.Id == challengeId);
            
            if (challenge != null && challenge.Tasks != null && challenge.Tasks.Any())
            {
                foreach (var task in challenge.Tasks)
                {
                    var userTask = new TaskItem
                    {
                        UserName = userName,
                        Name = $"[CHALLENGE] {task.Name}", 
                        Description = $"{challenge.Name}: {task.Description}",
                        Category = task.Category,
                        IsCompleted = false
                    };
                    await _taskService.AddTaskAsync(userTask);
                }
            }
        }

        public Task<List<Challenge>> GetChallengesByCategoryAsync(string category)
        {
            if (category == "ALL")
                return GetAllChallengesAsync();
            
            var filtered = _challenges.Where(c => c.Category == category).ToList();
            return Task.FromResult(filtered);
        }
    } // <-- Closes the ChallengeService class
} // <-- Closes the PawGress.Services namespace