namespace ToDoList.Models
{
    public class EntriesList
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Entry> Entries { get; set; }
    }
}
