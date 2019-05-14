using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OCFX.Areas.Identity.Data;
using OCFX.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCFX_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpectreController : ControllerBase
    {
        private readonly OCFXContext _context;

        public SpectreController(OCFXContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET api/spectre
        [HttpGet("[action]")]
        public List<Profile> Profiles()
        {
            var profiles = _context.Profiles.ToList();
            return profiles;
        }

        // Get api/spectre/gyms
        [HttpGet("GetGyms")]
        public async Task<List<Gym>> GetGyms()
        {
            List<Gym> gyms = await _context.Gyms.ToListAsync();
            return gyms;
        }

        // Get api/spectre/gyms
        [HttpGet("[action]")]
        public Array GetValues()
        {
            int[] jumble = { 8, 9, 10 };
            return jumble;
        }

        // GET api/spectre/5
        [HttpGet("{id}")]
        public async Task<Profile> GetAsync(int id)
        {
            Profile profile = await _context.Profiles.SingleAsync(i => i.Id == id);
            return profile;
        }

        // POST api/spectre
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/spectre/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/spectre/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
