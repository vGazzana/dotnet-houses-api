using Microsoft.EntityFrameworkCore;
using respTest1.Models;

namespace respTest1.Contexts
{

	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<Houses> Houses { get; set; }
	}

}