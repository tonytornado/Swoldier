using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static OCFX.DataModels.Workout;

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

        public List<Exercise> Exercises { get; set; }

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

	// An exercise can be a part of several workouts?
	public class Exercise
	{
		[Key]
		[Display(Name = "Exercise")]
		public int Id { get; set; }
		[Display(Name = "Exercise Name")]
		public string ExName { get; set; }
		[Display(Name = "Exercise Type")]
		public ExerciseType ExType { get; set; }
		[Display(Name = "Muscles targeted")]
		public WorkoutType ExGroup { get; set; }

		// An exercise can be a part of several workouts?
		//public List<WorkoutProgram> Exercises { get; set; }

		public enum ExerciseType
		{
			[Display(Name ="Cardio")]
			Cardio = 1,
			[Display(Name = "Strength")]
			Strength = 2,
			[Display(Name = "Balance")]
			Balance = 3,
			[Display(Name = "Flexibility")]
			Flexibility = 4,
			[Display(Name = "Tabata")]
			Tabata = 5
		}
	}

	// A relation table for the many to many relationship of the workouts to exercises.
	public class WorkoutProgram
	{
		[Key]
		[Display(Name = "Workout / Exercise Relation")]
		public int WorkoutProgramId { get; set; }

		[Display(Name = "Exercise ID's")]
		public int ExerciseId { get; set; }
		[Display(Name = "Exercises")]
		[ForeignKey("ExerciseId")]
		public Exercise Exercise { get; set; }

		[Display(Name = "Workout Name")]
		public int WorkoutId { get; set; }
		[ForeignKey("WorkoutId")]
		public Workout Workout { get; set; }
	}
}