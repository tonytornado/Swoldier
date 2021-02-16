using FitLibrary.Models.Fit;
using SocialLibrary.Profile;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FitLibrary.Models
{
    public class Journal
    {
        public int JournalId { get; set; }
        public List<WorkoutLog> Workouts { get; set; }

    }

    public class WorkoutLog
    {
        public int WorkoutLogId { get; set; }
        public int Duration { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        
        public int AssociatedWorkoutId { get; set; }
        [JsonIgnore]
        public Workout AssociatedWorkout { get; set; }

        public int ProfileId { get; set; }
        [JsonIgnore]
        public ProfileData Profile { get; set; }

    }
}
