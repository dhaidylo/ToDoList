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
        ApplicationContext db;

        public HomeController(ApplicationContext context)
        {
            db = context;

            if(!db.EntriesLists.Any())
            {
                var list = new EntriesList { Name = "General" };
                db.EntriesLists.Add(list);
                db.SaveChanges();
            }
        }

        public ActionResult Index(int? entriesList)
        {
            IQueryable<Entry> entries = db.Entries.Include(p => p.EntryList);
            if (entriesList != null && entriesList != 0)
            {
                entries = entries.Where(p => p.ListId == entriesList);
            }

            List<EntriesList> entriesLists = db.EntriesLists.ToList();
            
            entriesLists.Insert(0, new EntriesList { Name = "All", Id = 0 });

            IndexViewModel viewModel = new IndexViewModel
            {
                Entries = entries.ToList(),
                EntriesLists = new SelectList(entriesLists, "Id", "Name", entriesList),
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Index()
        {
            return View();
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
