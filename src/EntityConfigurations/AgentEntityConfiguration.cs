using EnquiryRouting.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnquiryRouting.Api.EntityConfigurations
{
	internal class AgentEntityConfiguration : IEntityTypeConfiguration<Agent>
	{
		public void Configure(EntityTypeBuilder<Agent> builder)
		{
			builder.ToTable(nameof(ApplicationDbContext.Agents));

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id)
				.ValueGeneratedNever();

			builder.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(x => x.MaxCapacity)
				.IsRequired();

			builder.Property(x => x.Status)
				.IsRequired()
				.HasConversion<int>();

			// Agent -> Languages (1-M)
			builder.HasMany(x => x.Languages)
				.WithOne()
				.HasForeignKey(x => x.AgentId)
				.OnDelete(DeleteBehavior.Cascade);
			
			// Agent -> Enquiries (1-M)
			builder.HasMany(x => x.Enquiries)
				.WithOne()
				.HasForeignKey(x => x.AgentId)
				.IsRequired(false)
				.OnDelete(DeleteBehavior.SetNull);

			// Agent -> Skills (M-M)
			builder.HasMany(x => x.Skills)
				.WithMany()
				.UsingEntity<Dictionary<string, object>>(
					"AgentSkill",
					right => right
						.HasOne<Skill>()
						.WithMany()
						.HasForeignKey("SkillId")
						.OnDelete(DeleteBehavior.Cascade),
					left => left
						.HasOne<Agent>()
						.WithMany()
						.HasForeignKey("AgentId")
						.OnDelete(DeleteBehavior.Cascade),
					join =>
					{
						join.ToTable("AgentSkills");
						join.HasKey("AgentId", "SkillId");
						join.HasIndex("SkillId", "AgentId");
					});
		}
	}
}
