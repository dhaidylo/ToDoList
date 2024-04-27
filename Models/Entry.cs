namespace ToDoList.Models
{
    public class Entry
    {
        int Id { get; set; }
        int ListId { get; set; }
        string? Text { get; set; }
        bool IsDone { get; set; }
    };
}
