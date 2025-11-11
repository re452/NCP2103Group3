using Microsoft.EntityFrameworkCore;
using PawGress.Data;
using PawGress.Models;

namespace PawGress.Services
{
    public class ChallengeService
    {
        private readonly AppDbContext _context;
        private readonly PetService _petService;

        public ChallengeService(AppDbContext context, PetService petService)
        {
            _context = context;
            _petService = petService;
        }

        // Get all challenges
        public async Task<List<Challenge>> GetAllChallengesAsync()
        {
            return await _context.Challenges
                .Include(c => c.Tasks)
                .ToListAsync();
        }

        // Get a single challenge by ID
        public async Task<Challenge?> GetChallengeByIdAsync(int id)
        {
            return await _context.Challenges
                .Include(c => c.Tasks)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        // Complete a challenge: mark tasks done + award XP to pet
        public async Task<bool> CompleteChallengeAsync(int challengeId, int petId)
        {
            var challenge = await _context.Challenges
                .Include(c => c.Tasks)
                .FirstOrDefaultAsync(c => c.Id == challengeId);

            if (challenge == null) return false;

            foreach (var task in challenge.Tasks)
            {
                if (!task.Completed)
                {
                    task.Completed = true;

                    // Award XP for each task to pet
                    await _petService.AddXPAsync(petId, task.XP);
                }
            }

            challenge.Completed = true; // optional flag
            await _context.SaveChangesAsync();
            return true;
        }

        // Add a pre-made or custom challenge
        public async Task<Challenge> AddChallengeAsync(string name, List<int> taskIds, bool isPreMade = false)
        {
            // Get the tasks selected by user
            var tasks = await _context.Tasks
                .Where(t => taskIds.Contains(t.Id))
                .ToListAsync();

            var challenge = new Challenge
            {
                Name = name,
                IsPreMade = isPreMade,
                Tasks = tasks
            };

            _context.Challenges.Add(challenge);
            await _context.SaveChangesAsync();
            return challenge;
        }
        public async Task<bool> CompleteChallengeForUserAsync(int userId, int challengeId, int petId)
        {
            var userChallenge = await _context.UserChallenges
                .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.ChallengeId == challengeId);

            if (userChallenge == null)
            {
                userChallenge = new UserChallenge
                {
                    UserId = userId,
                    ChallengeId = challengeId
                };
                _context.UserChallenges.Add(userChallenge);
            }

            if (userChallenge.Completed) return false;

            var challenge = await _context.Challenges
                .Include(c => c.Tasks)
                .FirstOrDefaultAsync(c => c.Id == challengeId);

            if (challenge == null) return false;

            foreach (var task in challenge.Tasks)
            {
            // await CompleteTaskForUserAsync(userId, task.Id, petId);
            }

            userChallenge.Completed = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}


