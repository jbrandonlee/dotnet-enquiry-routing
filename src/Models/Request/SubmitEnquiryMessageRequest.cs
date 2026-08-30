using EnquiryRouting.Api.Entities;
using EnquiryRouting.Api.Enums;

namespace EnquiryRouting.Api.Models.Request
{
	public class SubmitEnquiryMessageRequest
	{
		public Guid EnquiryId { get; set; }
		public required string SenderName { get; set; }
		public required string SenderType { get; set; }
		public required string Message { get; set; }
	}

	public static class SubmitEnquiryMessageRequestExtensions
	{
		public static ChatMessage ToDomainModel(this SubmitEnquiryMessageRequest dto)
		{
			MessageSenderType senderTypeEnum = Enum.Parse<MessageSenderType>(dto.SenderType);
			var message = new ChatMessage(dto.SenderName, senderTypeEnum, dto.Message);
			return message;
		}
	}
}
