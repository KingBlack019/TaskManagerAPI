using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions options) : base(options)
		{
		}

		// Tables
		public DbSet<TaskItem> Tasks { get; set; }
		public DbSet<User> Users { get; set; }
	}
}