using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Models;

namespace MyApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
  private readonly DatabaseContext _context;

  public TodosController(DatabaseContext context)
  {
    _context = context;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Todo>>> GetTodoItems()
  {
    return await _context.Todos.ToListAsync();
  }

  [HttpGet("complete")]
  public async Task<ActionResult<IEnumerable<Todo>>> GetCompletedTodoItems()
  {
    return await _context.Todos.Where(t => t.Completed).ToListAsync();
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Todo>> GetTodoItem(int id)
  {
    var todoItem = await _context.Todos.FindAsync(id);

    if (todoItem == null)
    {
      return NotFound();
    }

    return todoItem;
  }

  [HttpPost]
  public async Task<ActionResult<Todo>> PostTodoItem(Todo todo)
  {
    _context.Todos.Add(todo);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetTodoItem), new { id = todo.Id }, todo);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> PutTodoItem(int id, Todo todo)
  {
    if (id != todo.Id)
    {
      return BadRequest();
    }

    _context.Entry(todo).State = EntityState.Modified;
    await _context.SaveChangesAsync();

    return NoContent();
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteTodoItem(int id)
  {
    var todoItem = await _context.Todos.FindAsync(id);
    if (todoItem == null)
    {
      return NotFound();
    }

    _context.Todos.Remove(todoItem);
    await _context.SaveChangesAsync();

    return NoContent();
  }
}