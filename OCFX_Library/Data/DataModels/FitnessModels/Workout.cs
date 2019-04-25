using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OCFX.DataModels
{

    // A workout can have many exercises.
    // Each workout is static and cannot be changed.

    public class Workout
    {
        [Key]
        [Display(Name = "Workout")]
        public int Id { get; set; }

        [Display(Name = "Workout Program Title")]
        public string Title { get; set; }

        [Display(Name = "Workout Description")]
        public string Description { get; set; }

        [Display(Name = "Workout Duration")]
        public int Duration { get; set; }

        [Display(Name = "Target")]
        public WorkoutType TargetedMuscles { get; set; }

        [Display(Name = "Date Added")]
        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }

        public enum WorkoutType
        {
            [Display(Name = "Upper Body")]
            UpperBody = 1,
            [Display(Name = "Lower Body")]
            LowerBody = 2,
            [Display(Name = "Total Body")]
            TotalBody = 3,
            [Display(Name = "Chest")]
            Chest = 4,
            [Display(Name = "Back")]
            Back = 5,
            [Display(Name = "Shoulders")]
            Shoulders = 6,
            [Display(Name = "Legs")]
            Legs = 7,
            [Display(Name = "Interval Training")]
            HIIT = 8,
            [Display(Name = "Bodyweight")]
            Bodyweight = 9,
            [Display(Name = "Stretch")]
            Stretch = 10
        }
    }
}