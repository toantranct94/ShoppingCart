using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.API;

namespace ShoppingCart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackContext _context;

        public FeedbackController(FeedbackContext context)
        {
            _context = context;
        }

        // GET: api/Feedback
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedbackItem>>> GetFeedbackItems()
        {
          if (_context.FeedbackItems == null)
          {
              return NotFound();
          }
            return await _context.FeedbackItems.ToListAsync();
        }

        // GET: api/Feedback/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FeedbackItem>> GetFeedbackItem(Guid id)
        {
          if (_context.FeedbackItems == null)
          {
              return NotFound();
          }
            var feedbackItem = await _context.FeedbackItems.FindAsync(id);

            if (feedbackItem == null)
            {
                return NotFound();
            }

            return feedbackItem;
        }

        // PUT: api/Feedback/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedbackItem(Guid id, FeedbackItem feedbackItem)
        {
            if (id != feedbackItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(feedbackItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbackItemExists(id))
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

        // POST: api/Feedback
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FeedbackItem>> PostFeedbackItem(FeedbackItem feedbackItem)
        {
            if (_context.FeedbackItems == null)
            {
                return Problem("Entity set 'FeedbackContext.FeedbackItems'  is null.");
            }

            if (!IsValidEmail(feedbackItem.Email))
            {
                return BadRequest("Invalid email address.");
            }

            _context.FeedbackItems.Add(feedbackItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeedbackItem", new { id = feedbackItem.Id }, feedbackItem);
        }

        // DELETE: api/Feedback/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedbackItem(Guid id)
        {
            if (_context.FeedbackItems == null)
            {
                return NotFound();
            }
            var feedbackItem = await _context.FeedbackItems.FindAsync(id);
            if (feedbackItem == null)
            {
                return NotFound();
            }

            _context.FeedbackItems.Remove(feedbackItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FeedbackItemExists(Guid id)
        {
            return (_context.FeedbackItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // This regex pattern checks for a basic valid email format.
            // This is a simplified pattern and might not cover all edge cases.
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
