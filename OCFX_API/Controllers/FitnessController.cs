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
        public FitnessController(OCFXContext context) => this.Context = context ?? throw new ArgumentNullException(nameof(context));

        public OCFXContext Context { get; }

        /// <summary>
        /// Gets the workouts from the DBContext
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Workout> GetWorkouts() => Context.Workouts.ToList();
    }
}