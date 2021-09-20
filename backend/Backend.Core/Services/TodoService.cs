using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Core.Interfaces;
using Backend.Core.Models;
using Backend.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Core.Services
{
    public class TodoService : ITodoService
    {
        private ITodoRepository _repository;
        
        public TodoService(ITodoRepository repository)
        {
            _repository = repository;
        }
        public async Task<TodoModel> CreateTodoAsync(TodoModel todo)
        {
            if (todo is null)
            {
                throw new AccessViolationException(nameof(todo));
            }

            var todoEntity = new Data.Entities.Todo
            {
                Description = todo.Description,
                IsCompleted = todo.IsCompleted,
            };

            todoEntity = await _repository.AddAsync(todoEntity);

            return new TodoModel
            {
                Id = todoEntity.Id,
                Description = todo.Description,
                IsCompleted = todo.IsCompleted,
            };
        }

        public async Task DeleteTodoAsync(Guid todoId)
        {
            await _repository.RemoveAsync(todoId);
        }

        public async Task<TodoModel> GetTodoAsync(Guid todoId)
        {
            var todo = await _repository.FindAsync(todoId);
            
            if (todo is null)
            {
                throw new AccessViolationException(nameof(todo));
            }

            return new TodoModel
            {
                Id = todo.Id,
                Description = todo.Description,
                IsCompleted = todo.IsCompleted,
            };
        }

        public async Task<List<TodoModel>> GetTodosAsync()
        {
            IQueryable<Data.Entities.Todo> query = _repository.Get();
            return await query.Select(todo => new TodoModel
            {
                Id = todo.Id,
                Description = todo.Description,
                IsCompleted = todo.IsCompleted,
            })
            .ToListAsync();
        }

        public async Task<TodoModel> UpdateTodoAsync(TodoModel todo)
        {
            var todoEntity = new Data.Entities.Todo
            {
                Id = todo.Id,
                Description = todo.Description,
                IsCompleted = todo.IsCompleted,
            };

            todoEntity = await _repository.UpdateAsync(todoEntity);

            return new TodoModel
            {
                Id = todoEntity.Id,
                Description = todoEntity.Description,
                IsCompleted = todoEntity.IsCompleted,
            };
        }
    }
}