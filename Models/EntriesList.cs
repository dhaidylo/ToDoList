namespace ToDoList.Models
{
    public class EntriesList
    {
        int Id { get; set; }
        string? Name { get; set; }
        public ICollection<Entry> Entries { get; set; }
    }
}
