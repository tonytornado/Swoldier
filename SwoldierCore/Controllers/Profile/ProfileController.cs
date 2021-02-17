using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialLibrary.Data;
using SocialLibrary.DataModels;
using SocialLibrary.Models.Profile;
using SocialLibrary.Profile;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SwoldierCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly SocialDB _context;
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(SocialDB context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Profile
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfileData>>> GetProfiles()
        {
            return await _context.ProfileData.ToListAsync();
        }

        // GET: api/Profile/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ProfileData> GetProfileBaseAsync(int id)
        {
            ProfileData profileBase = await _context.ProfileData.FindAsync(id);
            return profileBase ?? null;
        }

        // PUT: api/Profile/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfile(int id, ProfileData profileBase)
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
        public async Task<ActionResult<ProfileData>> PostProfile(ProfileData profileBase)
        {
            _context.ProfileData.Add(profileBase);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfileBase", new { id = profileBase.Id }, profileBase);
        }

        [Authorize]
        [HttpPost]
        public ActionResult PostPhoto([FromForm] Photo file)
        {
            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/profiles/images/", file.FileName);
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    file.FormFile.CopyTo(stream);
                }

                _context.Photos.Add(file);

                return StatusCode(200);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        // DELETE: api/Profile/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            var profileBase = await _context.ProfileData.FindAsync(id);
            if (profileBase == null)
            {
                return NotFound();
            }

            _context.ProfileData.Remove(profileBase);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfileBaseExists(int id)
        {
            return _context.ProfileData.Any(e => e.Id == id);
        }


        // GET: api/Profile/userway
        // [Authorize]
        [HttpGet("userway")]
        public async Task<ProfileData> GetProfileBaseByUsername(string username)
        {
            var id = await _userManager.FindByNameAsync(username);
            var profilebase = await GetProfileFromUsername(id);

            return profilebase ?? null;
        }

        private async Task<ProfileData> GetProfileFromUsername(AppUser user)
        {
            return await _context.ProfileData.FirstOrDefaultAsync(u => u.User == user);
        }
    }
}
