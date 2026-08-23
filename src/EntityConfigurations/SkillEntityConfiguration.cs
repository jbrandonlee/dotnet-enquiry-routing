using EnquiryRouting.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnquiryRouting.Api.EntityConfigurations
{
	internal class SkillEntityConfiguration : IEntityTypeConfiguration<Skill>
	{
		public void Configure(EntityTypeBuilder<Skill> builder)
		{
			builder.ToTable(nameof(ApplicationDbContext.Skills));

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id)
				.ValueGeneratedNever();

			builder.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(x => x.IsPriority)
				.IsRequired();

			builder.HasIndex(x => x.Name)
				.IsUnique();
		}
	}
}
