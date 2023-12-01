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
    public class EstateLikesController : ControllerBase
    {
        private readonly EstateWebApiContext _context;

        public EstateLikesController(EstateWebApiContext context)
        {
            _context = context;
        }

        // GET: api/EstateLikes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstateLike>>> GetEstateLikes()
        {
          if (_context.EstateLikes == null)
          {
              return NotFound();
          }
            return await _context.EstateLikes.ToListAsync();
        }

        // GET: api/EstateLikes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstateLike>> GetEstateLike(long id)
        {
          if (_context.EstateLikes == null)
          {
              return NotFound();
          }
            var estateLike = await _context.EstateLikes.FindAsync(id);

            if (estateLike == null)
            {
                return NotFound();
            }

            return estateLike;
        }

        // PUT: api/EstateLikes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstateLike(long id, EstateLike estateLike)
        {
            if (id != estateLike.Id)
            {
                return BadRequest();
            }

            _context.Entry(estateLike).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstateLikeExists(id))
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

        // POST: api/EstateLikes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EstateLike>> PostEstateLike(EstateLikeDto estateLike)
        {
            if (_context.EstateLikes == null)
            {
                return Problem("Entity set 'EstateWebApiContext.EstateLikes'  is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var el = new EstateLike()
            {
                UserId = estateLike.UserId,
                EstateId = estateLike.EstateId
            };

            _context.EstateLikes.Add(el);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstateLike", new { id = el.Id }, el);
        }

        // DELETE: api/EstateLikes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstateLike(long id)
        {
            if (_context.EstateLikes == null)
            {
                return NotFound();
            }
            var estateLike = await _context.EstateLikes.FindAsync(id);
            if (estateLike == null)
            {
                return NotFound();
            }

            _context.EstateLikes.Remove(estateLike);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstateLikeExists(long id)
        {
            return (_context.EstateLikes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
