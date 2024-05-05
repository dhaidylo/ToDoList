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

        public IActionResult Index()
        {
            var entriesLists = _context.EntriesLists.ToList();
            ViewBag.EntriesLists = new SelectList(entriesLists, "Id", "Name");

            return View();
        }

        public IActionResult GetTasks(int? listId)
        {
            var entries = GetEntriesInList(listId);
            return PartialView("_TasksListPartial", entries.ToList());
        }

        private IQueryable<Entry> GetEntriesInList(int? listId)
        {
            IQueryable<Entry> entries = _context.Entries.Include(p => p.EntryList);
            if (listId != null && listId != 0)
            {
                entries = entries.Where(p => p.ListId == listId);
            }

            return entries;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTaskStatus(int? id)
        {
            if (id != null)
            {
                Entry? entry = await _context.Entries.FirstOrDefaultAsync(p => p.Id == id);
                if (entry != null)
                {
                    entry.IsDone = !entry.IsDone;
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([Bind("Text", "ListId")] Entry entry)
        {
            _context.Entries.Add(entry);
            await _context.SaveChangesAsync();
            return GetTasks(entry.ListId);
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
                    return GetTasks(entry.ListId);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateList([Bind("Name")] EntriesList entriesList)
        {
            _context.EntriesLists.Add(entriesList);
            await _context.SaveChangesAsync();
            ViewBag.EntriesLists = new SelectList(_context.EntriesLists, "Id", "Name");
            //var list = _context.EntriesLists.FirstOrDefault(p => p.Name == entriesList.Name);
            return Json(entriesList);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteList(int? id)
        {
            if (id != null)
            {
                EntriesList? list = await _context.EntriesLists.FirstOrDefaultAsync(p => p.Id == id);
                if (list != null)
                {
                    _context.EntriesLists.Remove(list);
                    await _context.SaveChangesAsync();
                    return NoContent();
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
