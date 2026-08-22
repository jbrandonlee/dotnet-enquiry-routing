using EnquiryRouting.Api.Enums;
using EnquiryRouting.Api.Utils;

namespace EnquiryRouting.Api.Entities
{
	public class ChatMessage
	{
		public Guid Id { get; set; } // Primary Key
		public Guid EnquiryId { get; set; } // Foreign Key
		public Guid SenderId { get; set; }
		public MessageSenderType SenderType { get; set; }
		public string Message { get; set; }
		public DateTimeOffset DateTimeCreated {  get; set; }

		public ChatMessage(Guid senderId, MessageSenderType senderType, string message)
		{
			Id = Guid.NewGuid();
			SenderId = senderId;
			SenderType = senderType;
			Message = message;
			DateTimeCreated = CommonUtils.SgtNow;
		}
	}
}
