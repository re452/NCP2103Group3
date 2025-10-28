using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PawGress.Models
{
    public class Challenge
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        // True if pre-made by devs, false if user-created
        public bool IsPreMade { get; set; } = true;

        // Navigation property for tasks in this challenge
        public List<TaskItem> Tasks { get; set; } = new();

        // Optional: track if challenge has been completed by a user
        public bool Completed { get; set; } = false;
    }
}
