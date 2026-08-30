using EnquiryRouting.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnquiryRouting.Api.EntityConfigurations
{
	internal class ChatMessageEntityConfiguration : IEntityTypeConfiguration<ChatMessage>
	{
		public void Configure(EntityTypeBuilder<ChatMessage> builder)
		{
			builder.ToTable(nameof(ApplicationDbContext.ChatMessages));

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id)
				.ValueGeneratedNever();

			builder.Property(x => x.EnquiryId)
				.IsRequired();

			builder.Property(x => x.SenderName)
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(x => x.SenderType)
				.IsRequired()
				.HasConversion<int>();

			builder.Property(x => x.Message)
				.IsRequired()
				.HasMaxLength(4000);

			builder.Property(x => x.DateTimeCreated)
				.IsRequired();

			// Efficient retrieval of a conversation in chronological order
			builder.HasIndex(x => new { x.EnquiryId, x.DateTimeCreated });
		}
	}
}
