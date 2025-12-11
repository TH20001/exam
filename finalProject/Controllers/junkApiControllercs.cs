using finalProject.Data;
using finalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace finalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JunkApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public JunkApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Junk>>> Get()
        {
            return await _context.Junk.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Junk>> Get(int id)
        {
            var junk = await _context.Junk.FindAsync(id);
            if (junk == null) return NotFound();
            return junk;
        }

        [HttpPost]
        public async Task<ActionResult<Junk>> Post(Junk junk)
        {
            _context.Junk.Add(junk);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = junk.Id }, junk);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Junk junk)
        {
            if (id != junk.Id) return BadRequest();

            _context.Entry(junk).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Junk.Any(e => e.Id == id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var junk = await _context.Junk.FindAsync(id);
            if (junk == null) return NotFound();

            _context.Junk.Remove(junk);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
