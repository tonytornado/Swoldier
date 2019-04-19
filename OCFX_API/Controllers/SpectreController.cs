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
        [HttpGet]
        public async Task<List<Profile>> Get()
        {
            var profiles = await _context.Profiles.ToListAsync();
            return profiles;
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
