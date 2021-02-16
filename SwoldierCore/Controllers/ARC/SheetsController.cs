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
    public class SheetsController : ControllerBase
    {
        private readonly ArcDB _context;

        public SheetsController(ArcDB context)
        {
            _context = context;
        }

        // GET: api/Sheets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sheet>>> GetSheet()
        {
            return await _context.Sheet.ToListAsync();
        }

        // GET: api/Sheets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sheet>> GetSheet(int id)
        {
            var sheet = await _context.Sheet.FindAsync(id);

            if (sheet == null)
            {
                return NotFound();
            }

            return sheet;
        }

        // PUT: api/Sheets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSheet(int id, Sheet sheet)
        {
            if (id != sheet.Id)
            {
                return BadRequest();
            }

            _context.Entry(sheet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SheetExists(id))
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

        // POST: api/Sheets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sheet>> PostSheet(Sheet sheet)
        {
            _context.Sheet.Add(sheet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSheet", new { id = sheet.Id }, sheet);
        }

        // DELETE: api/Sheets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSheet(int id)
        {
            var sheet = await _context.Sheet.FindAsync(id);
            if (sheet == null)
            {
                return NotFound();
            }

            _context.Sheet.Remove(sheet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SheetExists(int id)
        {
            return _context.Sheet.Any(e => e.Id == id);
        }
    }
}
