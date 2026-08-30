using EnquiryRouting.Api.Entities;
using EnquiryRouting.Api.Enums;

namespace EnquiryRouting.Api.Models.Request
{
	public class SubmitEnquiryMessageRequest
	{
		public Guid EnquiryId { get; set; }
		public Guid MessageId { get; set; }
		public required string SenderName { get; set; }
		public required int SenderType { get; set; }
		public required string Message { get; set; }
	}

	public static class SubmitEnquiryMessageRequestExtensions
	{
		public static ChatMessage ToDomainModel(this SubmitEnquiryMessageRequest dto)
		{
			var message = new ChatMessage(dto.MessageId, dto.SenderName, (MessageSenderType)dto.SenderType, dto.Message);
			return message;
		}
	}
}
