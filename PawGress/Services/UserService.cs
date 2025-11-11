using Microsoft.EntityFrameworkCore;
using PawGress.Data;
using PawGress.Models;
using System.Security.Cryptography;
using System.Text;

namespace PawGress.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        // 🔒 Hash password with SHA256
        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        // ✅ Create new user (hashed password)
        public async Task<User?> CreateUserAsync(string username, string password)
        {
            Console.WriteLine($"[UserService] Creating user: {username}");

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (existingUser != null)
                return null;

            var hashed = HashPassword(password);
            var user = new User { Username = username, Password = hashed };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        // ✅ Authenticate (compare with hashed)
        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            var hashed = HashPassword(password);
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == hashed);
        }

        // ✅ Get user by username
        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
