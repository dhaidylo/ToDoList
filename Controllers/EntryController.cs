﻿using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int? id)
        {
            if (id != null)
            {
                Entry? entry = await _context.Entries.FirstOrDefaultAsync(p => p.Id == id);
                if (entry != null)
                {
                    entry.IsDone = !entry.IsDone;
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Text", "ListId")] Entry entry)
        {
            _context.Entries.Add(entry);
            await _context.SaveChangesAsync();
            return NoContent();
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
                    return NoContent();
                }
            }
            return NotFound();
        }
    }
}
