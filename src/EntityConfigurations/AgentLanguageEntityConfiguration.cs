using EnquiryRouting.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnquiryRouting.Api.EntityConfigurations
{
	internal class AgentLanguageEntityConfiguration : IEntityTypeConfiguration<AgentLanguage>
	{
		public void Configure(EntityTypeBuilder<AgentLanguage> builder)
		{
			builder.ToTable(nameof(ApplicationDbContext.AgentLanguages));

			builder.HasKey(x => new { x.AgentId, x.LanguageCode });

			builder.Property(x => x.LanguageCode)
				.IsRequired()
				.HasConversion<int>();

			builder.HasIndex(x => new { x.LanguageCode, x.AgentId });
		}
	}
}
