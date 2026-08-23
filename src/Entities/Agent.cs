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

		private readonly ICollection<AgentLanguage> _languages = new HashSet<AgentLanguage>();
		public IReadOnlyCollection<AgentLanguage> Languages => (IReadOnlyCollection<AgentLanguage>)_languages;

		private readonly ICollection<Skill> _skills = new HashSet<Skill>();
		public IReadOnlyCollection<Skill> Skills => (IReadOnlyCollection<Skill>)_skills;

		private readonly ICollection<Enquiry> _enquiries = new List<Enquiry>();
		public IReadOnlyCollection<Enquiry> Enquiries => (IReadOnlyCollection<Enquiry>)_enquiries;

		#region Derived Properties
		[NotMapped]
		public int RemainingCapacity => MaxCapacity - Enquiries.Count(e => !e.IsClosed);

		[NotMapped]
		public bool IsAvailable => RemainingCapacity > 0 && Status == AgentStatus.Online;
		#endregion

		public Agent(string name, int maxCapacity)
		{
			Name = name;
			MaxCapacity = maxCapacity;
			Status = AgentStatus.Offline;
		}

		public void AddLanguages(IEnumerable<AgentLanguage> languages)
		{
			_languages.AddRange(languages);
		}

		public void AddSkills(IEnumerable<Skill> skills)
		{
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
