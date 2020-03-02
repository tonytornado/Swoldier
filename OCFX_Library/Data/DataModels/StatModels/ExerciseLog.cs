using System;
using System.ComponentModel.DataAnnotations;

namespace OCFX.DataModels
{
    /// <summary>
    /// Exercise Logging Class
    /// </summary>
    public class ExerciseLog
    {
        public ExerciseLog()
        {
        }

        public ExerciseLog(ProfileSheet profile, Exercise exercise, int set, int reps)
        {
            Profile = profile ?? throw new ArgumentNullException(nameof(profile));
            Exercise = exercise ?? throw new ArgumentNullException(nameof(exercise));
            Set = set;
            Reps = reps;
        }

        public int Id { get; set; }
        /// <summary>
        /// Associated Profile
        /// </summary>
        public ProfileSheet Profile { get; set; }
        /// <summary>
        /// Associated Exercise being logged
        /// </summary>
        [Display(Name = "Exercise")]
        public Exercise Exercise { get; set; }
        /// <summary>
        /// Sets done for the exercise
        /// </summary>
        [Display(Name = "Set")]
        public int Set { get; set; }
        /// <summary>
        /// Repetitions of the exercise.
        /// Generally 1-20, anything more is overkill
        /// </summary>
        [Display(Name = "Repetitions"), Range(1, 20)]
        public int Reps { get; set; }
    }
}