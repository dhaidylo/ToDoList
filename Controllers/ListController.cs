using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class ListController : Controller
    {
        private readonly ApplicationContext _context;

        public ListController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name")] EntriesList entriesList)
        {
            _context.EntriesLists.Add(entriesList);
            await _context.SaveChangesAsync();
            ViewBag.EntriesLists = new SelectList(_context.EntriesLists, "Id", "Name");
            return Json(entriesList);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                EntriesList? list = await _context.EntriesLists.FirstOrDefaultAsync(p => p.Id == id);
                if (list != null)
                {
                    var entries = _context.Entries.Where(p => p.ListId == id);
                    _context.RemoveRange(entries);
                    _context.EntriesLists.Remove(list);
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }

            return NotFound();
        }
    }
}
