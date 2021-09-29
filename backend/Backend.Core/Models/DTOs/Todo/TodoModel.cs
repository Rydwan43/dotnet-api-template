using System;

namespace Backend.Core.Models.DTOs.Todo
{
    public class TodoModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}