using EnquiryRouting.Api.Enums;

namespace EnquiryRouting.Api.Entities
{
	public class ChatMessage
	{
		public Guid Id { get; set; } // Primary Key
		public Guid EnquiryId { get; set; } // Foreign Key
		public string SenderName { get; set; }
		public MessageSenderType SenderType { get; set; }
		public string Message { get; set; }
		public DateTimeOffset DateTimeCreated { get; set; }

		public ChatMessage(string senderName, MessageSenderType senderType, string message)
		{
			Id = Guid.NewGuid();
			SenderName = senderName;
			SenderType = senderType;
			Message = message;
			DateTimeCreated = DateTimeOffset.UtcNow;
		}
	}
}
