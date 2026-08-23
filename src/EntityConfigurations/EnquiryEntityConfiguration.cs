using EnquiryRouting.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnquiryRouting.Api.EntityConfigurations
{
	internal class EnquiryEntityConfiguration : IEntityTypeConfiguration<Enquiry>
	{
		public void Configure(EntityTypeBuilder<Enquiry> builder)
		{
			builder.ToTable(nameof(ApplicationDbContext.Enquiries));

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id)
				.ValueGeneratedNever();

			builder.Property(x => x.LanguageCode)
				.IsRequired()
				.HasConversion<int>();

			builder.Property(x => x.AgentId)
				.IsRequired(false);

			builder.Property(x => x.DateTimeCreated)
				.IsRequired();

			builder.Property(x => x.DateTimeClosed)
				.IsRequired(false);

			builder.Property(x => x.CreatedBy)
				.IsRequired();

			builder.Property(x => x.ClosedBy)
				.IsRequired(false);

			// Enquiry -> Agent
			builder.HasOne<Agent>()
				.WithMany(x => x.Enquiries)
				.HasForeignKey(x => x.AgentId)
				.IsRequired(false)
				.OnDelete(DeleteBehavior.SetNull);

			// Enquiry -> ChatMessages
			builder.HasMany(x => x.Messages)
				.WithOne()
				.HasForeignKey(x => x.EnquiryId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			// Enquiry -> Skills
			builder.HasMany(x => x.RequiredSkills)
				.WithMany()
				.UsingEntity<Dictionary<string, object>>(
					"EnquirySkill",
					right => right
						.HasOne<Skill>()
						.WithMany()
						.HasForeignKey("SkillId")
						.OnDelete(DeleteBehavior.Cascade),
					left => left
						.HasOne<Enquiry>()
						.WithMany()
						.HasForeignKey("EnquiryId")
						.OnDelete(DeleteBehavior.Cascade),
					join =>
					{
						join.ToTable("EnquirySkills");
						join.HasKey("EnquiryId", "SkillId");
						join.HasIndex("SkillId", "EnquiryId");
					});
		}
	}
}
