namespace EnquiryRouting.Api.Entities
{
	public class Skill
	{
		public Guid Id { get; set; }
		public required string Name { get; set; }
		public bool IsPriority { get; set; }

		// private readonly ICollection<Agent> _agents = new List<Agent>();
		// public IReadOnlyCollection<Agent> Agents => _agents.ToList().AsReadOnly();
	}
}
