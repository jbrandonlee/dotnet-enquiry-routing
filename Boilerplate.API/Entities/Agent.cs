using EnquiryRouting.Api.Enums;
using EnquiryRouting.Api.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnquiryRouting.Api.Entities
{
	public class Agent
	{
		public Guid Id { get; private set; }
		public string Name { get; private set; }
		public int MaxCapacity { get; private set; }
		public AgentStatus Status { get; private set; }

		private readonly ICollection<LanguageCode> _languages = new HashSet<LanguageCode>();
		public IReadOnlyCollection<LanguageCode> Languages => _languages.ToList().AsReadOnly();

		private readonly ICollection<Skill> _skills = new HashSet<Skill>();
		public IReadOnlyCollection<Skill> Skills => _skills.ToList().AsReadOnly();

		private readonly ICollection<Enquiry> _enquiries = new List<Enquiry>();
		public IReadOnlyCollection<Enquiry> Enquiries => _enquiries.ToList().AsReadOnly();

		#region Derived Properties
		[NotMapped]
		public int RemainingCapacity => MaxCapacity - Enquiries.Count(e => !e.IsClosed);

		[NotMapped]
		public bool IsAvailable => RemainingCapacity > 0 && Status == AgentStatus.Online;
		#endregion

		public Agent(string name, int maxCapacity, IEnumerable<LanguageCode> languages, IEnumerable<Skill> skills)
		{
			Name = name;
			MaxCapacity = maxCapacity;
			Status = AgentStatus.Offline;
			_languages.AddRange(languages);
			_skills.AddRange(skills);
		}

		public void AssignEnquiry(Enquiry enquiry)
		{
			if (!enquiry.IsPending)
				throw new InvalidOperationException("Enquiry is already assigned.");

			if (!IsAvailable)
				throw new InvalidOperationException("Agent has no remaining capacity or is not online.");

			_enquiries.Add(enquiry);
		}

		public void UpdateStatus(string status)
		{
			var statusEnum = Enum.Parse<AgentStatus>(status);
			Status = statusEnum;
		}
	}
}
