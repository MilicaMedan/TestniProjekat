using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend;

namespace Backend.Priority
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriorityController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PriorityController(AppDbContext context)
        {
            _context = context;
        }
       
        // GET: api/Priority
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Priority>>> GetPriorities()
        {
            return await _context.Priorities.ToListAsync();
        }
        
       [HttpGet("{id}")]
       public async Task<ActionResult<Priority>> GetPriority(int id)
       {
           var priority = await _context.Priorities.FindAsync(id);

           if (priority == null)
           {
               return NotFound();
           }

           return priority;
       }

       [HttpPut("{id}")]
       public async Task<IActionResult> PutPriority(int id, Priority priority)
       {
           if (id != priority.id)
           {
               return BadRequest();
           }

           _context.Entry(priority).State = EntityState.Modified;

           try
           {
               await _context.SaveChangesAsync();
           }
           catch (DbUpdateConcurrencyException)
           {
               if (!PriorityExists(id))
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

       [HttpPost]
       public async Task<ActionResult<Priority>> PostPriority(Priority priority)
       {
           _context.Priorities.Add(priority);
           await _context.SaveChangesAsync();

           return CreatedAtAction("GetPriority", new { id = priority.id }, priority);
       }

       [HttpDelete("{id}")]
       public async Task<ActionResult<Priority>> DeletePriority(int id)
       {
           var priority = await _context.Priorities.FindAsync(id);
           if (priority == null)
           {
               return NotFound();
           }

           _context.Priorities.Remove(priority);
           await _context.SaveChangesAsync();

           return priority;
       }

       private bool PriorityExists(int id)
       {
           return _context.Priorities.Any(e => e.id == id);
       }
    }
}
