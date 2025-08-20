using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Data;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TasksController : ControllerBase
	{
		private readonly AppDbContext _context;

		public TasksController(AppDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks()
		{
			return await _context.Tasks.Include(t => t.User).ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks(int id)
		{
			var task = await _context.Tasks.FindAsync(id);

			if(task == null) 
				return NotFound();

			return Ok(task);
		}

		[HttpPost]
		public async Task<ActionResult<TaskItem>> PostTask(TaskItem taskItem)
		{
			_context.Tasks.Add(taskItem);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetTasks), new {id = taskItem.Id});
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> PutTask(int id, TaskItem taskItem)
		{
			if (id != taskItem.Id)
				return BadRequest();

			_context.Entry(taskItem).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException dbException) 
			{
				if (!_context.Tasks.Any(t => t.Id == id))
					return NotFound();
				else
					throw;
			}

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTask(int id)
		{
			var task = await _context.Tasks.FindAsync(id);

			if (task == null) 
				return NotFound();

			_context.Tasks.Remove(task);
			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}
