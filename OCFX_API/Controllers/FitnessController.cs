using Microsoft.AspNetCore.Mvc;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OCFX_API.Controllers
{
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
        public List<Workout> GetWorkouts() => Context.Workouts.ToList();
    }
}