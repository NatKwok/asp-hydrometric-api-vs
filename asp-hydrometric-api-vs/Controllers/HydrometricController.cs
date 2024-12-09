using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using asp_hydrometric_api_vs.Models;
using Microsoft.Build.Framework;
using Newtonsoft.Json;
using GeoJSON.Text.Feature;
using GeoJSON.Text.Geometry;
using static System.Runtime.InteropServices.JavaScript.JSType;


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
            var feature = await _context.HydrometricAnnualPeaks.ToListAsync();

            //Map data to GeoJSON Features
            var features = feature.Select(record =>
            {

                // Check if Geom is not null
                if (record.Geom == null)
                {
                    // Handle cases where Geom is null (e.g., skip the record or provide a default value)
                    return null;
                }

                // Replace with the actual fields for latitude and longitude in your model
                var latitude = record.Geom.X;
                var longitude = record.Geom.Y;

                // Create a GeoJSON Point geometry
                var point = new Point(new Position(latitude, longitude));

                // Add additional properties from your model
                var properties = new Dictionary<string, object>
                    {
                        { "Id", record.Id },
                        { "Name", record.StationName }, // Example field
                        { "Peak", record.Peak } // Example field
                    };

                // Create a GeoJSON Feature
                return new Feature(point, properties);
            }).ToList();

            //Create a FeatureCollection
            var featureCollection = new FeatureCollection(features);

            //Serialize to GeoJSON
            var geoJson = JsonConvert.SerializeObject(featureCollection);

            // Return GeoJSON with appropriate content type
            return Content(geoJson, "application/json");
        }

        // GET: api/Hydrometric/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HydrometricAnnualPeak>> GetHydrometricAnnualPeak(string id)
        {
            var feature = await _context.HydrometricAnnualPeaks.FindAsync(id);

            if (feature == null)
            {
                return NotFound();
            }

                // Check if Geom is not null
                if (feature.Geom == null)
                {
                    // Handle cases where Geom is null (e.g., skip the record or provide a default value)
                    return null;
                }

                // Replace with the actual fields for latitude and longitude in your model
                var latitude = feature.Geom.X;
                var longitude = feature.Geom.Y;

                // Create a GeoJSON Point geometry
                var point = new Point(new Position(latitude, longitude));

                // Add additional properties from your model
                var properties = new Dictionary<string, object>
                    {
                        { "Id", feature.Id },
                        { "Name", feature.StationName }, // Example field
                        { "Peak", feature.Peak } // Example field
                    };

            var features = new Feature(point, properties);

            //Serialize to GeoJSON
            var geoJson = JsonConvert.SerializeObject(features);

            // Return GeoJSON with appropriate content type
            return Content(geoJson, "application/json");
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
