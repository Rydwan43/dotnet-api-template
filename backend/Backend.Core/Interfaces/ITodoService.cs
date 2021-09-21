using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Core.Models.Todo;

namespace Backend.Core.Interfaces
{
    public interface ITodoService
    {
        Task<TodoModel> CreateTodoAsync(TodoModel todo);
        Task<TodoModel> GetTodoAsync(Guid todoId);
        Task<List<TodoModel>> GetTodosAsync();
        Task<TodoModel> UpdateTodoAsync(TodoModel todo);
        Task DeleteTodoAsync(Guid todoId);

    }
}