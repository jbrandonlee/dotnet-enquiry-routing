using Boilerplate.API.Entities;
using Boilerplate.API.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Boilerplate.API
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<Movie> Movies { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new MovieEntityConfiguration());
		}
	}
}
