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
    public class EstateCommentsController : ControllerBase
    {
        private readonly EstateWebApiContext _context;

        public EstateCommentsController(EstateWebApiContext context)
        {
            _context = context;
        }

        // GET: api/EstateComments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstateComment>>> GetEstateComments()
        {
          if (_context.EstateComments == null)
          {
              return NotFound();
          }
            return await _context.EstateComments.ToListAsync();
        }

        // GET: api/EstateComments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstateComment>> GetEstateComment(long id)
        {
          if (_context.EstateComments == null)
          {
              return NotFound();
          }
            var estateComment = await _context.EstateComments.FindAsync(id);

            if (estateComment == null)
            {
                return NotFound();
            }

            return estateComment;
        }

        // PUT: api/EstateComments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstateComment(long id, EstateComment estateComment)
        {
            if (id != estateComment.Id)
            {
                return BadRequest();
            }

            _context.Entry(estateComment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstateCommentExists(id))
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

        // POST: api/EstateComments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EstateComment>> PostEstateComment(EstateCommentDto estateComment)
        {
            if (_context.EstateComments == null)
            {
                return Problem("Entity set 'EstateWebApiContext.EstateComments'  is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ec = new EstateComment()
            {
                Text = estateComment.Text,
                UserId = estateComment.UserId,
                EstateId = estateComment.EstateId
            };

            _context.EstateComments.Add(ec);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstateComment", new { id = ec.Id }, ec);
        }

        // DELETE: api/EstateComments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstateComment(long id)
        {
            if (_context.EstateComments == null)
            {
                return NotFound();
            }
            var estateComment = await _context.EstateComments.FindAsync(id);
            if (estateComment == null)
            {
                return NotFound();
            }

            _context.EstateComments.Remove(estateComment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstateCommentExists(long id)
        {
            return (_context.EstateComments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
