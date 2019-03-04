﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OCFX.DataModels
{
    // A relation table for the many to many relationship of the workouts to exercises.
    public class WorkoutProgram
    {
        [Key]
        [Display(Name = "Workout / Exercise Relation")]
        public int WorkoutProgramId { get; set; }

        [Display(Name = "Exercise ID's")]
        public int ExerciseId { get; set; }
        [ForeignKey("ExerciseId")]
        public Exercise Exercise { get; set; }

        [Display(Name = "Workout Name")]
        public int WorkoutId { get; set; }
        [ForeignKey("WorkoutId")]
        public Workout Workout { get; set; }

        [Display(Name = "Campaign Name")]
        public int CampaignId { get; set; }
        [ForeignKey("CampaignId")]
        public Campaign Campaign { get; set; }

        [Display(Name = "Sets")]
        public int Sets { get; set; }

        [Display(Name = "Reps")]
        [Range(1, 20, ErrorMessage = "Uhhh....")]
        public int Repetitions { get; set; }
    }
}