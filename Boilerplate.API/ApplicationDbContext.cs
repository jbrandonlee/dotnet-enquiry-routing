using EnquiryRouting.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnquiryRouting.Api
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<Agent> Agents { get; set; }
		public DbSet<ChatMessage> ChatMessages { get; set; }
		public DbSet<Enquiry> Enquiries { get; set; }
		public DbSet<Skill> Skills { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// modelBuilder.ApplyConfiguration(new AgentEntityConfiguration());
			// modelBuilder.ApplyConfiguration(new ChatMessageEntityConfiguration());
			// modelBuilder.ApplyConfiguration(new EnquiryEntityConfiguration());
			// modelBuilder.ApplyConfiguration(new SkillEntityConfiguration());
		}
	}
}
