using EnquiryRouting.Api.Enums;
using EnquiryRouting.Api.Extensions;

namespace EnquiryRouting.Api.Entities
{
	public class Agent
	{
		public Guid Id { get; private set; }
		public string Name { get; private set; }
		public int Capacity { get; private set; }
		public AgentStatus Status { get; private set; }

		private readonly ICollection<LanguageCode> _languages = new HashSet<LanguageCode>();
		public IReadOnlyCollection<LanguageCode> Languages => _languages.ToList().AsReadOnly();

		private readonly ICollection<Skill> _skills = new HashSet<Skill>();
		public IReadOnlyCollection<Skill> Skills => _skills.ToList().AsReadOnly();

		private readonly ICollection<Enquiry> _enquiries = new List<Enquiry>();
		public IReadOnlyCollection<Enquiry> Enquiries => _enquiries.ToList().AsReadOnly();

		public Agent(string name, int capacity, IEnumerable<LanguageCode> languages, IEnumerable<Skill> skills)
		{
			Name = name;
			Capacity = capacity;
			Status = AgentStatus.Offline;
			_languages.AddRange(languages);
			_skills.AddRange(skills);
		}

		public void AssignEnquiry(Enquiry enquiry)
		{
			enquiry.Assign(this.Id);
			//_enquiries.Add(enquiry);
		}

		public void UpdateStatus(string status)
		{
			var statusEnum = Enum.Parse<AgentStatus>(status);
			Status = statusEnum;
		}
	}
}
