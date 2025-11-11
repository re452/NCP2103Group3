using System.ComponentModel.DataAnnotations;

namespace PawGress.Models
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int XP { get; set; }

        // Category: Upper, Lower, Core, Cardio
        public string Category { get; set; } = "All";

        // True if this task is a habit
        public bool IsHabit { get; set; } = false;

        public int UserId { get; set; } = 0; // 0 if global

        public bool Completed { get; set; } = false;
    }
}
