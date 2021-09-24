using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Backend.Core.Interfaces;
using Backend.Core.Models.Helpers;
using Backend.Core.Models.Todo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;


        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet("{id}")]
        [ActionName("GetTodoAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoModel>> GetTodoAsync(Guid id)
        {
            var todo = await _todoService.GetTodoAsync(id);

            if (todo is null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TodoModel>> GetTodosAsync()
        {
            var todos = await _todoService.GetTodosAsync();
            return Ok(todos);
        }

        [HttpGet("page")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginationModel<TodoModel>>> GetTodosAsync([Required]int page, [Required]int pageSize)
        {
            var todos = await _todoService.GetPaginationTodosAsync(page, pageSize);
            return Ok(todos);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<TodoModel>> CreateTodoAsync(CreateTodoModel createTodoModel)
        {
            var todoModel = new TodoModel
            {
                Name = createTodoModel.Name,
                Description = createTodoModel.Description,
                IsCompleted = createTodoModel.IsCompleted
            };

            var createdTodo = await _todoService.CreateTodoAsync(todoModel);

            return CreatedAtAction(nameof(GetTodoAsync), new { id = createdTodo.Id }, createdTodo);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateTodoAsync(Guid id, TodoModel updateTodoModel)
        {
            if (id != updateTodoModel.Id)
            {
                return BadRequest();
            }

            var todo = await _todoService.GetTodoAsync(id);

            if (todo is null)
                return NotFound();

            var todoModel = new TodoModel
            {
                Id = id,
                Name = updateTodoModel.Name,
                Description = updateTodoModel.Description,
                IsCompleted = updateTodoModel.IsCompleted,
            };

            await _todoService.UpdateTodoAsync(todoModel);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteTodoAsync(Guid id)
        {
            var todo = await _todoService.GetTodoAsync(id);
            if (todo is null)
            {
                return NotFound();
            }

            await _todoService.DeleteTodoAsync(id);
            return NoContent();
        }
    }
}