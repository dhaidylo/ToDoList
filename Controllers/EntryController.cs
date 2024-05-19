using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class EntryController : Controller
    {
        private readonly ApplicationContext _context;

        public EntryController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStatus([FromBody] int? id)
        {
            if (id != null)
            {
                Entry? entry = await _context.Entries.FindAsync(id);
                if (entry != null)
                {
                    entry.IsDone = !entry.IsDone;
                    await _context.SaveChangesAsync();
                    return Ok();
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Entry entry)
        {
            _context.Entries.Add(entry);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] int? id)
        {
            if (id != null)
            {
                Entry? entry = await _context.Entries.FindAsync(id);
                if (entry != null)
                {
                    _context.Entries.Remove(entry);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
            }
            return NotFound();
        }
    }
}
