using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ToDoList.Models;

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

        public IActionResult GetTasks([FromBody] int? listId)
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
