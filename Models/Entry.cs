namespace ToDoList.Models
{
    public record class Entry (int Id, int ListId, string Text, bool IsDone);
}
