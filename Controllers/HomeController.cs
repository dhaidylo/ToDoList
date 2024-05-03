using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ToDoList.Models;
using ToDoList.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        ApplicationContext _context;

        public HomeController(ApplicationContext context)
        {
            _context = context;

            if(!_context.EntriesLists.Any())
            {
                var list = new EntriesList { Name = "General" };
                _context.EntriesLists.Add(list);
                _context.SaveChanges();
            }
        }

        public IActionResult Index(int? entriesList)
        {
            IQueryable<Entry> entries = _context.Entries.Include(p => p.EntryList);
            if (entriesList != null && entriesList != 0)
            {
                entries = entries.Where(p => p.ListId == entriesList);
            }

            List<EntriesList> entriesLists = _context.EntriesLists.ToList();

            entriesLists.Insert(0, new EntriesList { Name = "All", Id = 0 });

            IndexViewModel viewModel = new IndexViewModel
            {
                Entries = entries.ToList(),
                EntriesLists = new SelectList(entriesLists, "Id", "Name", entriesList),
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Entry? entry = await _context.Entries.FirstOrDefaultAsync(p => p.Id == id);
                if (entry != null)
                {
                    _context.Entries.Remove(entry);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
