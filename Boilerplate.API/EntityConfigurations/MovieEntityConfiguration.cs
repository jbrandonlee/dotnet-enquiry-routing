using EnquiryRouting.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnquiryRouting.Api.EntityConfigurations
{
	public class MovieEntityConfiguration : IEntityTypeConfiguration<Movie>
	{
		public void Configure(EntityTypeBuilder<Movie> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id)
				.HasDefaultValueSql("gen_random_uuid()");

			builder.Property(x => x.Title)
				.HasMaxLength(255)
				.IsRequired(true);
		}
	}
}
