using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwoldierCore.Data;
using SwoldierCore.Data.Profile;
using SwoldierCore.Models;

namespace SwoldierCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Profile
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfileBase>>> GetProfiles()
        {
            return await _context.ProfileBase.ToListAsync();
        }

        // GET: api/Profile/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ProfileBase> GetProfileBaseAsync(int id)
        {
            ProfileBase profileBase = await _context.ProfileBase.FindAsync(id);
            return profileBase ?? null;
        }

        // PUT: api/Profile/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfile(int id, ProfileBase profileBase)
        {
            if (id != profileBase.Id)
            {
                return BadRequest();
            }

            _context.Entry(profileBase).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileBaseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Profile
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ProfileBase>> PostProfile(ProfileBase profileBase)
        {
            _context.ProfileBase.Add(profileBase);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfileBase", new { id = profileBase.Id }, profileBase);
        }

        // DELETE: api/Profile/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            var profileBase = await _context.ProfileBase.FindAsync(id);
            if (profileBase == null)
            {
                return NotFound();
            }

            _context.ProfileBase.Remove(profileBase);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfileBaseExists(int id)
        {
            return _context.ProfileBase.Any(e => e.Id == id);
        }


        // GET: api/Profile/userway
        // [Authorize]
        [HttpGet("userway")]
        public async Task<ProfileBase> GetProfileBaseByUsername(string username)
        {
            var id = await _userManager.FindByNameAsync(username);
            var profilebase = await GetProfileFromUsername(id);
            
            return profilebase ?? null;
        }

        private async Task<ProfileBase> GetProfileFromUsername(ApplicationUser user)
        {
            return await _context.ProfileBase.FirstOrDefaultAsync(u => u.User == user);
        }
    }
}
