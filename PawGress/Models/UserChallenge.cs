using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PawGress.Models;

namespace PawGress.Models
{
    public class UserChallenge
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public int ChallengeId { get; set; }
        public bool Completed { get; set; } = false;

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [ForeignKey("ChallengeId")]
        public Challenge? Challenge { get; set; }
    }
}
