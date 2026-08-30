using EnquiryRouting.Api.Entities;
using EnquiryRouting.Api.Enums;

namespace EnquiryRouting.Api.Models.Response
{
	public class ChatMessageViewModel
	{
		public string SenderName { get; set; } = string.Empty;
		public MessageSenderType SenderType { get; set; }
		public string Message { get; set; } = string.Empty;
		public DateTimeOffset DateTimeCreated { get; set; }
	}

	public static class ChatMessageViewModelExtensions
	{
		public static ChatMessageViewModel ToViewModel(this ChatMessage chatMessage)
		{
			return new ChatMessageViewModel
			{
				SenderName = chatMessage.SenderName,
				SenderType = chatMessage.SenderType,
				Message = chatMessage.Message,
				DateTimeCreated = chatMessage.DateTimeCreated
			};
		}
	}
}
