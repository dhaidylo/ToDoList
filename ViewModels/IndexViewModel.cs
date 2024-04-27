using Microsoft.AspNetCore.Mvc.Rendering;
using ToDoList.Models;

namespace ToDoList.ViewModels
{
    public class IndexViewModel
    {
        public SelectList EntriesLists { get; set; } = new SelectList(new List<EntriesList>(), "Id", "Name");
        public IEnumerable<Entry> Entries { get; set; } = new List<Entry>();
    }
}
