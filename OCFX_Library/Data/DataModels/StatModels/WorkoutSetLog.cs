using System;

namespace OCFX.DataModels
{
    /// <summary>
    /// The Relation Table for WorkoutSets
    /// </summary>
    public class WorkoutSetLog
    {
        public int Id { get; set; }
        public Workout Workout { get; set; }
        public ProfileSheet Profile { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public int Duration { get; set; }
    }
}