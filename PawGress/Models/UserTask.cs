using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PawGress.Models;

namespace PawGress.Models
{
    public class UserTask
    {
        [Key]
        public int Id { get; set; }

        // The user who owns this task
        public int UserId { get; set; }

        // The task itself
        public int TaskItemId { get; set; }

        // Completion status
        public bool Completed { get; set; } = false;

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [ForeignKey("TaskItemId")]
        public TaskItem? TaskItem { get; set; }
    }
}
