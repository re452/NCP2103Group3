// Models/Challenge.cs
using System.Collections.Generic;

namespace PawGress.Models
{
    public class Challenge
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty; // e.g., /Images/challenge1.png
        public string Category { get; set; } = string.Empty; // e.g., "UPPER BODY"
        public string Difficulty { get; set; } = "MEDIUM"; // "EASY", "MEDIUM", "HARD"
        public string MainMuscle { get; set; } = string.Empty;
        public string SecondaryMuscles { get; set; } = string.Empty;
        public bool IsSelected { get; set; } = false; // For the 'TASKS SELECTED' counter
        
        // --- MISSING PROPERTIES ADDED TO FIX BUILD ERRORS ---
        public bool IsPreMade { get; set; } = true; // Indicates if it's a static challenge
        public bool Completed { get; set; } = false; // Indicates if the user completed the whole challenge
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>(); // A challenge may contain multiple tasks
    }
}