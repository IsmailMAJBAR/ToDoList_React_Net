// Models/TodoItem.cs
namespace todo_list_server.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string? Description { get; set; }
    }
}