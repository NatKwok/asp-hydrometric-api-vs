using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using asp_hydrometric_api_vs.Models;

namespace asp_hydrometric_api_vs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HydrometricController : ControllerBase
    {
        private readonly HydrometricContext _context;

        public HydrometricController(HydrometricContext context)
        {
            _context = context;
        }

        // GET: api/Hydrometric
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HydrometricAnnualPeak>>> GetHydrometricAnnualPeaks()
        {
            return await _context.HydrometricAnnualPeaks.ToListAsync();
        }

        // GET: api/Hydrometric/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HydrometricAnnualPeak>> GetHydrometricAnnualPeak(string id)
        {
            var hydrometricAnnualPeak = await _context.HydrometricAnnualPeaks.FindAsync(id);

            if (hydrometricAnnualPeak == null)
            {
                return NotFound();
            }

            return hydrometricAnnualPeak;
        }

        // PUT: api/Hydrometric/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHydrometricAnnualPeak(string id, HydrometricAnnualPeak hydrometricAnnualPeak)
        {
            if (id != hydrometricAnnualPeak.Id)
            {
                return BadRequest();
            }

            _context.Entry(hydrometricAnnualPeak).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HydrometricAnnualPeakExists(id))
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

        // POST: api/Hydrometric
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HydrometricAnnualPeak>> PostHydrometricAnnualPeak(HydrometricAnnualPeak hydrometricAnnualPeak)
        {
            _context.HydrometricAnnualPeaks.Add(hydrometricAnnualPeak);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HydrometricAnnualPeakExists(hydrometricAnnualPeak.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetHydrometricAnnualPeak", new { id = hydrometricAnnualPeak.Id }, hydrometricAnnualPeak);
        }

        // DELETE: api/Hydrometric/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHydrometricAnnualPeak(string id)
        {
            var hydrometricAnnualPeak = await _context.HydrometricAnnualPeaks.FindAsync(id);
            if (hydrometricAnnualPeak == null)
            {
                return NotFound();
            }

            _context.HydrometricAnnualPeaks.Remove(hydrometricAnnualPeak);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HydrometricAnnualPeakExists(string id)
        {
            return _context.HydrometricAnnualPeaks.Any(e => e.Id == id);
        }
    }
}
