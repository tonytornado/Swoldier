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
        public CaloriesBurned(int calorieCount, string activity, int time, ProfileSheet profile)
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
        public CaloriesBurned(string activity, int time, ProfileSheet profile)
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
        public ProfileSheet Profile { get; set; }
    }
}