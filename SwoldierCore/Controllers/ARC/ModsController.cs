using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArcLibrary.Data;
using ArcLibrary.DataModels.CharacterModels;

namespace SwoldierCore.Controllers.ARC
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModsController : ControllerBase
    {
        private readonly ArcDB _context;

        public ModsController(ArcDB context)
        {
            _context = context;
        }

        // GET: api/Mods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mod>>> GetMod()
        {
            return await _context.Mod.ToListAsync();
        }

        // GET: api/Mods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mod>> GetMod(int id)
        {
            var mod = await _context.Mod.FindAsync(id);

            if (mod == null)
            {
                return NotFound();
            }

            return mod;
        }

        // PUT: api/Mods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMod(int id, Mod mod)
        {
            if (id != mod.Id)
            {
                return BadRequest();
            }

            _context.Entry(mod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModExists(id))
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

        // POST: api/Mods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Mod>> PostMod(Mod mod)
        {
            _context.Mod.Add(mod);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMod", new { id = mod.Id }, mod);
        }

        // DELETE: api/Mods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMod(int id)
        {
            var mod = await _context.Mod.FindAsync(id);
            if (mod == null)
            {
                return NotFound();
            }

            _context.Mod.Remove(mod);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModExists(int id)
        {
            return _context.Mod.Any(e => e.Id == id);
        }
    }
}
