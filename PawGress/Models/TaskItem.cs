using System.ComponentModel.DataAnnotations;

namespace PawGress.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        // USER
        [Required]
        public string UserName { get; set; } = string.Empty;

        // TASK BASICS
        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        // STATUS
        public bool IsCompleted { get; set; } = false;

        // EXTRA FIELDS
        public int XP { get; set; } = 0;
        public string Category { get; set; } = "General";
        public bool IsHabit { get; set; } = false;

        public int UserId { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
