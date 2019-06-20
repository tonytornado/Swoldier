using System;
using System.ComponentModel.DataAnnotations;

namespace OCFX.DataModels
{
    public class CaloriesBurned
    {
        public int Id { get; set; }
        public Profile Profile { get; set; }
        public int CalorieCount { get; set; }
        public string Activity { get; set; }
        public int Time { get; set; }
    }

    /// <summary>
    /// Log a weight along with a progress photo
    /// </summary>
    public class WeightMeasurement
    {
        public int Id { get; set; }
        public Profile Profile { get; set; }
        [Display(Name = "Progress Photo")]
        public Photo ProgressPhoto { get; set; }
        [Display(Name = "Photo Date")]
        public DateTime Date { get; set; }
        [Display(Name = "Weight (in kg)")]
        public int Weight { get; set; }
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


    public class WorkoutSet
    {

    }
}