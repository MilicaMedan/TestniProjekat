using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend;
using Backend.Helpers;
using Backend.Status;

namespace Backend.Taskk
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaskksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Taskk>> GetTaskk(int id)
        {
            var status = await _context.Tasks.FindAsync(id);

            if (status == null)
            {
                return NotFound();
            }

            return status;
        }

        [Authorize]
        [HttpGet("byUserId/{userId}")]
        public async Task<ActionResult<IEnumerable<TaskPom>>> GetTaskks(int userId)
        {
            List<Taskk> tasks = await _context.Tasks.ToListAsync();
            List<Taskk> tasks1 = tasks.Where(t => t.UserId == userId).ToList();
            List<TaskPom> taskspom = new List<TaskPom>();
            foreach (Taskk t in tasks1) {
                TaskPom pom = new TaskPom();
                pom.id = t.id;
                pom.end = t.endDate.ToString();
                pom.start = t.startDate.ToString();
                pom.note = t.note;
                pom.title = t.title;
                Status.Status status = await _context.Statuses.FindAsync(t.StatusId);
                pom.status = status.name;
                Priority.Priority priority = await _context.Priorities.FindAsync(t.PriorityId);
                pom.priority = priority.name;
                taskspom.Add(pom);
            }

            return taskspom;
        }

        private async Task<IActionResult> PutTaskk(int id, Taskk taskk)
        {
            if (id != taskk.id)
            {
                return BadRequest();
            }

            _context.Entry(taskk).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskkExists(id))
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

        private async Task<ActionResult<Taskk>> PostTaskk(Taskk taskk)
        {
            _context.Tasks.Add(taskk);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaskk", new { id = taskk.id }, taskk);
        }

        [Authorize]
        [ActionName("PostTaskPom")]
        [HttpPost("add")]
        public async Task<ActionResult<Taskk>> PostTaskPom(TaskPom task)
        {
            Taskk pom = new Taskk();
            pom.startDate = DateTime.Now;
            pom.endDate = DateTime.Now;
            pom.note = task.note;
            pom.title = task.title;
            Status.Status status = _context.Statuses.SingleOrDefault(x => x.name == task.status);
            pom.StatusId = status.id;
            Priority.Priority priority = _context.Priorities.SingleOrDefault(x => x.name == task.priority);
            pom.PriorityId = priority.id;
            pom.UserId = task.UserId;

            return await PostTaskk(pom);
        }

        [Authorize]
        [ActionName("PutTaskPom")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> PutTaskPom(int id, TaskPom task)
        {
            Taskk pom = new Taskk();
            pom.startDate = DateTime.Now;
            pom.endDate = DateTime.Now;
            pom.id = id;
            pom.note = task.note;
            pom.title = task.title;
            pom.UserId = task.UserId;
            Status.Status status = _context.Statuses.SingleOrDefault(x => x.name == task.status);
            pom.StatusId = status.id;
            Priority.Priority priority = _context.Priorities.SingleOrDefault(x => x.name == task.priority);
            pom.PriorityId = priority.id;
            return await PutTaskk(id, pom);
        }

        private bool TaskkExists(int id)
        {
            return _context.Tasks.Any(e => e.id == id);
        }
    }
}
