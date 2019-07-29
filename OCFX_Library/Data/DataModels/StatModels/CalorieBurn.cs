using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCFX.DataModels
{
    /// <summary>
    /// An item that charts calories burned by an exercise or workout
    /// </summary>
    public class CaloriesBurned
    {
        /// <summary>
        /// Standard implementation of a calories burned object
        /// </summary>
        /// <param name="calorieCount"></param>
        /// <param name="activity"></param>
        /// <param name="time"></param>
        /// <param name="profile"></param>
        public CaloriesBurned(int calorieCount, string activity, int time, Profile profile)
        {
            CalorieCount = calorieCount;
            Activity = activity ?? throw new ArgumentNullException(nameof(activity),"Invalid activity");
            Time = time;
            Profile = profile ?? throw new ArgumentNullException(nameof(profile));
        }

        /// <summary>
        /// Standard Implementation without Calorie Count of a calories burned object
        /// </summary>
        /// <param name="activity"></param>
        /// <param name="time"></param>
        /// <param name="profile"></param>
        public CaloriesBurned(string activity, int time, Profile profile)
        {
            Activity = activity ?? throw new ArgumentNullException(nameof(activity), "Invalid activity");
            Time = time;
            Profile = profile ?? throw new ArgumentNullException(nameof(profile));
        }

        public int Id { get; set; }
        /// <summary>
        /// Calories burned
        /// </summary>
        public int CalorieCount { get; set; }
        [Display(Name = "Activity Name")]
        public string Activity { get; set; }
        [Display(Name = "Time"), Range(1, 420)]
        public int Time { get; set; }

        [ForeignKey("ProfileId")]
        public Profile Profile { get; set; }
    }

    /// <summary>
    /// An log entry for a user's weight along with a progress photo (optional)
    /// </summary>
    public class WeightMeasurement
    {

        /// <summary>
        /// Standard Weight Measurement object
        /// </summary>
        public WeightMeasurement()
        {
        }

        /// <summary>
        /// Standard implementation of a Weight Measurement
        /// </summary>
        /// <param name="date"></param>
        /// <param name="weight"></param>
        /// <param name="progressPhoto"></param>
        /// <param name="profile"></param>
        public WeightMeasurement(DateTime date, double weight, Photo progressPhoto, Profile profile)
        {
            Date = date;
            Weight = weight;
            ProgressPhoto = progressPhoto ?? throw new ArgumentNullException(nameof(progressPhoto));
            Profile = profile ?? throw new ArgumentNullException(nameof(profile));
        }

        public int Id { get; set; }
        /// <summary>
        /// An optional progress @Photo object
        /// </summary>
        [Display(Name = "Progress Photo")]
        public Photo ProgressPhoto { get; set; }
        /// <summary>
        /// The date that the photo was taken, added automatically in most cases.
        /// </summary>
        [Display(Name = "Photo Date")]
        public DateTime Date { get; set; }
        /// <summary>
        /// Current weight
        /// </summary>
        [Display(Name = "Weight")]
        [StringLength(4)]
        public double Weight { get; set; }
        /// <summary>
        /// The profile ID of the user making the change.
        /// </summary>
        [ForeignKey("ProfileId")]
        public Profile Profile { get; set; }
    }

    /// <summary>
    /// Exercise Logging Class
    /// </summary>
    public class ExerciseLog
    {
        public ExerciseLog()
        {
        }

        public ExerciseLog(Profile profile, Exercise exercise, int set, int reps)
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
        public Profile Profile { get; set; }
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

    /// <summary>
    /// The Relation Table for WorkoutSets
    /// </summary>
    public class WorkoutSetLog
    {
        public int Id { get; set; }
        public Workout Workout { get; set; }
        public Profile Profile { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public int Duration { get; set; }
    }
}