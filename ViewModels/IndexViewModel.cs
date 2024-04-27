using ToDoList.Models;

namespace ToDoList.ViewModels
{
    public class IndexViewModel
    {
        public User? User { get; set; }
        public IEnumerable<EntriesList> EntriesLists { get; set; } = new List<EntriesList>();
        public IEnumerable<Entry> Entries { get; set; } = new List<Entry>();
    }
}
