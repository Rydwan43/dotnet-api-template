namespace Backend.Core.Models.DTOs.Todo
{
    public class CreateTodoModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}