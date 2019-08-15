using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OCFX_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FitnessController : ControllerBase
    {
        private readonly OCFXContext Context;

        public FitnessController(OCFXContext context) => Context = context ?? throw new ArgumentNullException(nameof(context), "There ain't a DB, bro.");

        /// <summary>
        /// Gets the workouts from the DB
        /// </summary>
        /// <returns>List of all workouts loaded</returns>
        [HttpGet("[action]")]
        public List<Workout> GetWorkouts() => Context
            .Workouts
            .ToList();

        /// <summary>
        /// Gets all exercises from the DB
        /// </summary>
        /// <returns>List of all exercises loaded</returns>
        [HttpGet("[action]")]
        public List<Exercise> GetExercises() => 
            Context
            .Exercises
            .ToList();

        [HttpGet("ExerciseTypes")]
        public Array GetExerciseTypes() => Enum.GetValues(typeof(Exercise.ExerciseType));

        [HttpGet("MuscleTypes")]
        public Array GetTargetedMuscleGroups() => Enum.GetValues(typeof(Workout.WorkoutType));

        /// <summary>
        /// Load an exercise into the DB
        /// </summary>
        /// <param name="EName"></param>
        /// <param name="EType"></param>
        /// <param name="EMuscles"></param>
        /// <param name="EDescription"></param>
        [HttpPost("[action]")]
        public void PostExercise(string EName, Exercise.ExerciseType EType, Workout.WorkoutType EMuscles, string EDescription)
        {
            var plan = new Exercise(EName, EType, EMuscles, EDescription);
            Context.Exercises.Add(plan);
            Context.SaveChanges();
        }

        [HttpGet("[action]")]
        public List<WorkoutProgram> GetPrograms() => Context.WorkoutPrograms.ToList();
    }
}