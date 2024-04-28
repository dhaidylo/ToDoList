namespace ToDoList.Models
{
    public class Entry
    {
        public int Id { get; set; }
        public int ListId { get; set; }
        public string? Text { get; set; }
        public bool IsDone { get; set; }
        public EntriesList? EntryList { get; set; }
    };
}
