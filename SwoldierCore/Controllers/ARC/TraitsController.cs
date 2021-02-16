using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArcLibrary.Data;
using ArcLibrary.DataModels.CharacterModels;

namespace SwoldierCore.Controllers.ARC
{
    [Route("api/[controller]")]
    [ApiController]
    public class TraitsController : ControllerBase
    {
        private readonly ArcDB _context;

        public TraitsController(ArcDB context)
        {
            _context = context;
        }

        // GET: api/Traits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trait>>> GetTrait()
        {
            return await _context.Trait.ToListAsync();
        }

        // GET: api/Traits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trait>> GetTrait(int id)
        {
            var trait = await _context.Trait.FindAsync(id);

            if (trait == null)
            {
                return NotFound();
            }

            return trait;
        }

        // PUT: api/Traits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrait(int id, Trait trait)
        {
            if (id != trait.ID)
            {
                return BadRequest();
            }

            _context.Entry(trait).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TraitExists(id))
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

        // POST: api/Traits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Trait>> PostTrait(Trait trait)
        {
            _context.Trait.Add(trait);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrait", new { id = trait.ID }, trait);
        }

        // DELETE: api/Traits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrait(int id)
        {
            var trait = await _context.Trait.FindAsync(id);
            if (trait == null)
            {
                return NotFound();
            }

            _context.Trait.Remove(trait);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TraitExists(int id)
        {
            return _context.Trait.Any(e => e.ID == id);
        }
    }
}
