using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class EntriesListController : Controller
    {
        private readonly ApplicationContext _context;

        public EntriesListController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EntriesList entriesList)
        {
            _context.EntriesLists.Add(entriesList);
            await _context.SaveChangesAsync();
            return RedirectToAction("Create", "Entry");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? ListId)
        {
            if (ListId != null)
            {
                EntriesList? list = await _context.EntriesLists.FirstOrDefaultAsync(p => p.Id == ListId);
                if (list != null)
                {
                    foreach(var entry in _context.Entries)
                    {
                        if(entry.ListId == ListId)
                            _context.Remove(entry);
                    }
                    _context.EntriesLists.Remove(list);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Create","Entry");
                }
            }
            return NotFound();
        }
    }
}
