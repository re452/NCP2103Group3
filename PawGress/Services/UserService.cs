using Microsoft.EntityFrameworkCore;
using PawGress.Data;
using PawGress.Models;

namespace PawGress.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> CreateUserAsync(string username, string password)
        {
            var user = new User { Username = username, Password = password };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            Console.WriteLine($"[UserService] Created user: {username}");
            return user;
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
