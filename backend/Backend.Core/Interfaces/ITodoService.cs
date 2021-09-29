using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Core.Models.Helpers;
using Backend.Core.Models.DTOs.Todo;

namespace Backend.Core.Interfaces
{
    public interface ITodoService
    {
        Task<TodoModel> CreateTodoAsync(TodoModel todo);
        Task<TodoModel> GetTodoAsync(Guid todoId);
        Task<PaginationModel<TodoModel>> GetPaginationTodosAsync(int page, int pageSize);
        Task<List<TodoModel>> GetTodosAsync();
        Task<TodoModel> UpdateTodoAsync(TodoModel todo);
        Task DeleteTodoAsync(Guid todoId);

    }
}