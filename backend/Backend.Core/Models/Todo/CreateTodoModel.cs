namespace Backend.Core.Models.Todo
{
    public class CreateTodoModel
    {
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}