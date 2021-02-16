using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialLibrary.Data;
using SocialLibrary.Feed;

namespace SwoldierCore.Controllers.Profile
{
    [Route("api/[controller]")]
    [ApiController]
    public class WallsController : ControllerBase
    {
        private readonly SocialDB _context;

        public WallsController(SocialDB context)
        {
            _context = context;
        }

        // GET: api/Walls
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wall>>> GetWall()
        {
            return await _context.Wall.ToListAsync();
        }

        // GET: api/Walls/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Wall>> GetWall(int id)
        {
            var wall = await _context.Wall.FindAsync(id);

            if (wall == null)
            {
                return NotFound();
            }

            return wall;
        }

        // PUT: api/Walls/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWall(int id, Wall wall)
        {
            if (id != wall.WallId)
            {
                return BadRequest();
            }

            _context.Entry(wall).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WallExists(id))
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

        // POST: api/Walls
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Wall>> PostWall(Wall wall)
        {
            _context.Wall.Add(wall);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWall", new { id = wall.WallId }, wall);
        }

        // DELETE: api/Walls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWall(int id)
        {
            var wall = await _context.Wall.FindAsync(id);
            if (wall == null)
            {
                return NotFound();
            }

            _context.Wall.Remove(wall);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WallExists(int id)
        {
            return _context.Wall.Any(e => e.WallId == id);
        }
    }
}
