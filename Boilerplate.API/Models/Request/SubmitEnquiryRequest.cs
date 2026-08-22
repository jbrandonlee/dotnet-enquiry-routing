using EnquiryRouting.Api.Entities;
using EnquiryRouting.Api.Enums;

namespace EnquiryRouting.Api.Models.Request
{
	public class SubmitEnquiryRequest
	{
		public Guid ClientId { get; set; }
		public string LanguageCode { get; set; } = string.Empty;
		public string Message { get; set; } = string.Empty;
		public IEnumerable<string> RequiredSkills { get; set; } = new List<string>();
	}

	public static class SubmitEnquiryRequestExtensions
	{
		public static Enquiry ToDomainModel(this SubmitEnquiryRequest dto, IEnumerable<Skill> skills)
		{
			LanguageCode languageCodeEnum = Enum.Parse<LanguageCode>(dto.LanguageCode);
			var initalMessage = new ChatMessage(dto.ClientId, MessageSenderType.Client, dto.Message);
			var enquiry = new Enquiry(dto.ClientId, languageCodeEnum, initalMessage, skills);
			return enquiry;
		}
	}
}