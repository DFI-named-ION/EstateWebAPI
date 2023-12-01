using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EstateWebAPI.EF;
using EstateWebAPI.Models;
using EstateWebAPI.Models.DTO;

namespace EstateWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstatesController : ControllerBase
    {
        private readonly EstateWebApiContext _context;

        public EstatesController(EstateWebApiContext context)
        {
            _context = context;
        }

        // GET: api/Estates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estate>>> GetEstates()
        {
          if (_context.Estates == null)
          {
              return NotFound();
          }
            return await _context.Estates.ToListAsync();
        }

        // GET: api/Estates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estate>> GetEstate(long id)
        {
          if (_context.Estates == null)
          {
              return NotFound();
          }
            var estate = await _context.Estates.FindAsync(id);

            if (estate == null)
            {
                return NotFound();
            }

            return estate;
        }

        // PUT: api/Estates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstate(long id, Estate estate)
        {
            if (id != estate.Id)
            {
                return BadRequest();
            }

            _context.Entry(estate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstateExists(id))
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

        // POST: api/Estates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Estate>> PostEstate(EstateDto estate)
        {
            if (_context.Estates == null)
            {
                return Problem("Entity set 'EstateWebApiContext.Estates'  is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var e = new Estate()
            {
                Image = estate.Image,
                Title = estate.Title,
                Description = estate.Description,
                OwnerId = estate.OwnerId,
                Price = estate.Price,
                FloorCount = estate.FloorCount,
                RoomCount = estate.RoomCount,
                CategoryId = estate.CategoryId
            };

            _context.Estates.Add(e);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstate", new { id = e.Id }, e);
        }

        // DELETE: api/Estates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstate(long id)
        {
            if (_context.Estates == null)
            {
                return NotFound();
            }
            var estate = await _context.Estates.FindAsync(id);
            if (estate == null)
            {
                return NotFound();
            }

            _context.Estates.Remove(estate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstateExists(long id)
        {
            return (_context.Estates?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
