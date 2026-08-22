using EnquiryRouting.Api.Enums;
using EnquiryRouting.Api.Extensions;
using EnquiryRouting.Api.Utils;

namespace EnquiryRouting.Api.Entities
{
	public class Enquiry
	{
		public Guid Id { get; private set; } // Primary Key
		public LanguageCode LanguageCode { get; private set; }
		public EnquiryStatus Status { get; private set; }
		public Guid? AgentId { get; private set; } // Foreign Key (Optional)
		public DateTimeOffset DateTimeCreated { get; private set; }
		public DateTimeOffset? DateTimeClosed { get; private set; }
		public Guid CreatedBy { get; private set; }
		public Guid? ClosedBy { get; private set; }

		private readonly ICollection<ChatMessage> _messages = new List<ChatMessage>();
		public IReadOnlyCollection<ChatMessage> Messages => _messages.ToList().AsReadOnly();

		private readonly ICollection<Skill> _requiredSkills = new HashSet<Skill>();
		public IReadOnlyCollection<Skill> RequiredSkills => _requiredSkills.ToList().AsReadOnly();

		public Enquiry(Guid customerId, LanguageCode languageCode, ChatMessage initialMessage, IEnumerable<Skill> requiredSkills)
		{
			Id = Guid.NewGuid();
			LanguageCode = languageCode;
			Status = EnquiryStatus.Pending;
			AgentId = null;
			DateTimeCreated = CommonUtils.SgtNow;
			DateTimeClosed = null;
			CreatedBy = customerId;
			ClosedBy = null;

			_messages.Add(initialMessage);
			_requiredSkills.AddRange(requiredSkills);
		}

		public void Assign(Guid agentId)
		{
			if (Status != EnquiryStatus.Pending || AgentId is not null) { return; }
			Status = EnquiryStatus.Assigned;
			AgentId = agentId;
		}

		public void Close(Guid agentId)
		{
			Status = EnquiryStatus.Closed;
			DateTimeClosed = CommonUtils.SgtNow;
			ClosedBy = agentId;
		}

		public void AddMessage(ChatMessage chatMessage)
		{
			_messages.Add(chatMessage);
		}
	}
}
