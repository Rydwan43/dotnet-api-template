using System;

namespace Backend.Data.Entities
{
    public class Todo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public string UserId { get; set; }
    }
}