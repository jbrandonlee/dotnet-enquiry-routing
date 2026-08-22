using EnquiryRouting.Api.Enums;
using EnquiryRouting.Api.Extensions;
using EnquiryRouting.Api.Utils;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnquiryRouting.Api.Entities
{
	public class Enquiry
	{
		public Guid Id { get; private set; } // Primary Key
		public LanguageCode LanguageCode { get; private set; }
		public Guid? AgentId { get; private set; } // Foreign Key (Optional)
		public DateTimeOffset DateTimeCreated { get; private set; }
		public DateTimeOffset? DateTimeClosed { get; private set; }
		public Guid CreatedBy { get; private set; }
		public Guid? ClosedBy { get; private set; }

		private readonly ICollection<ChatMessage> _messages = new List<ChatMessage>();
		public IReadOnlyCollection<ChatMessage> Messages => _messages.ToList().AsReadOnly();

		private readonly ICollection<Skill> _requiredSkills = new HashSet<Skill>();
		public IReadOnlyCollection<Skill> RequiredSkills => _requiredSkills.ToList().AsReadOnly();

		#region Derived Properties
		[NotMapped]
		public bool IsUrgent => _requiredSkills.Any(x => x.IsPriority);

		[NotMapped]
		public bool IsPending => AgentId is null && !IsClosed;

		[NotMapped]
		public bool IsAssigned => AgentId is not null && !IsClosed;

		[NotMapped]
		public bool IsClosed => ClosedBy is not null;
		#endregion

		public Enquiry(Guid customerId, LanguageCode languageCode, ChatMessage initialMessage, IEnumerable<Skill> requiredSkills)
		{
			Id = Guid.NewGuid();
			LanguageCode = languageCode;
			AgentId = null;
			DateTimeCreated = CommonUtils.SgtNow;
			DateTimeClosed = null;
			CreatedBy = customerId;
			ClosedBy = null;

			_messages.Add(initialMessage);
			_requiredSkills.AddRange(requiredSkills);
		}

		public void SetToClosed(Guid agentId)
		{
			DateTimeClosed = CommonUtils.SgtNow;
			ClosedBy = agentId;
		}

		public void AddMessage(ChatMessage chatMessage)
		{
			_messages.Add(chatMessage);
		}
	}
}
