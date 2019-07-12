using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCFX.DataModels
{
    /// <summary>
    /// An item that charts calories burned by an exercise or workout
    /// </summary>
    public class CaloriesBurned
    {
        public int Id { get; set; }
        public Profile Profile { get; set; }
        public int CalorieCount { get; set; }
        public string Activity { get; set; }
        public int Time { get; set; }
    }

    /// <summary>
    /// An log entry for a user's weight along with a progress photo (optional)
    /// </summary>
    public class WeightMeasurement
    {
        public int Id { get; set; }
        [Display(Name = "Progress Photo")]
        public Photo ProgressPhoto { get; set; }
        [Display(Name = "Photo Date")]
        public DateTime Date { get; set; }
        [Display(Name = "Weight (in lbs.)")]
        [StringLength(4)]
        public double Weight { get; set; }
        [ForeignKey("ProfileId")]
        public Profile Profile { get; set; }
    }

    /// <summary>
    /// Exercise Logging Class
    /// </summary>
    public class ExerciseLog
    {
        public int Id { get; set; }
        public Profile Profile { get; set; }
        public Exercise Exercise { get; set; }
        public int Set { get; set; }
        public int Reps { get; set; }
    }

    /// <summary>
    /// The Relation Table for WorkoutSets
    /// </summary>
    public class WorkoutSetLog
    {
        public int Id { get; set; }
        public Workout Workout { get; set; }
        public ICollection<ExerciseLog> ExercisesLogged { get; set; }
        public DateTime Date { get; set; }
    }
}