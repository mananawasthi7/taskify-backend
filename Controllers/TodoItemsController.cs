using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskifyApi.Data;
using TaskifyApi.Models;

namespace TaskifyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodoItems), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, TodoItem item)
        {
            if (id != item.Id)
                return BadRequest();

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var todo = await _context.TodoItems.FindAsync(id);
            if (todo == null)
                return NotFound();

            _context.TodoItems.Remove(todo);
            await _context.SaveChangesAsync();

            return NoContent();

        }
        
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Taskify API is working ✅");
        }

    }
}
