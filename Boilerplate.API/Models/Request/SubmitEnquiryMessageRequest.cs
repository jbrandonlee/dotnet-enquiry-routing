using EnquiryRouting.Api.Entities;
using EnquiryRouting.Api.Enums;

namespace EnquiryRouting.Api.Models.Request
{
	public class SubmitEnquiryMessageRequest
	{
		public Guid EnquiryId { get; set; }
		public Guid SenderId { get; set; }
		public string SenderType { get; set; } = string.Empty;
		public string Message { get; set; } = string.Empty;
	}

	public static class SubmitEnquiryMessageRequestExtensions
	{
		public static ChatMessage ToDomainModel(this SubmitEnquiryMessageRequest dto)
		{
			MessageSenderType senderTypeEnum = Enum.Parse<MessageSenderType>(dto.SenderType);
			var message = new ChatMessage(dto.SenderId, senderTypeEnum, dto.Message);
			return message;
		}
	}
}
