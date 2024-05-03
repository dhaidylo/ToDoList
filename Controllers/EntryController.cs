using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var entriesList = _context.EntriesLists.ToList();
            var entriesSelectList = new SelectList(entriesList, "Id", "Name");

            ViewBag.EntriesLists = entriesSelectList;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Text", "ListId")] Entry entry)
        {
            _context.Entries.Add(entry);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
