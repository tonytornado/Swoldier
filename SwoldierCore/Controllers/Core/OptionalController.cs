using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialLibrary.Data;
using SocialLibrary.Profile;

namespace SwoldierCore.Controllers.Core
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionalController : ControllerBase
    {
        private readonly SocialDB _context;

        public OptionalController(SocialDB context)
        {
            _context = context;
        }

        // GET: api/Optional
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OptionData>>> GetOptionsData()
        {
            return await _context.OptionsData.ToListAsync();
        }

        // GET: api/Optional/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OptionData>> GetOptionData(int id)
        {
            var optionData = await _context.OptionsData.FindAsync(id);

            if (optionData == null)
            {
                return NotFound();
            }

            return optionData;
        }

        // PUT: api/Optional/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOptionData(int id, OptionData optionData)
        {
            if (id != optionData.OptionId)
            {
                return BadRequest();
            }

            _context.Entry(optionData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OptionDataExists(id))
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

        // POST: api/Optional
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OptionData>> PostOptionData(OptionData optionData)
        {
            _context.OptionsData.Add(optionData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOptionData", new { id = optionData.OptionId }, optionData);
        }

        // DELETE: api/Optional/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOptionData(int id)
        {
            var optionData = await _context.OptionsData.FindAsync(id);
            if (optionData == null)
            {
                return NotFound();
            }

            _context.OptionsData.Remove(optionData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OptionDataExists(int id)
        {
            return _context.OptionsData.Any(e => e.OptionId == id);
        }
    }
}
