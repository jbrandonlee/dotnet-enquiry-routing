using EnquiryRouting.Api.Enums;

namespace EnquiryRouting.Api.Entities
{
	public class AgentLanguage
	{
		public Guid AgentId { get; set; }
		public LanguageCode LanguageCode { get; set; }

		// public Agent Agent { get; set; } = null!;
	}
}
