using EnquiryRouting.Api.Entities;
using EnquiryRouting.Api.Enums;

namespace EnquiryRouting.Api.Models.Request
{
	public class CreateAgentsRequest
	{
		public IEnumerable<CreateAgentRequest> Agents { get; set; } = new List<CreateAgentRequest>();
	}

	public class CreateAgentRequest
	{
		public required string Name { get; set; }
		public int MaxCapacity { get; set; }
		public IEnumerable<string> Languages { get; set; } = new List<string>();
		public IEnumerable<string> Skills { get; set; } = new List<string>();
	}

	public static class CreateAgentRequestExtensions
	{
		public static Agent ToDomainModel(this CreateAgentRequest dto, HashSet<Skill> skills)
		{
			var agentLanguages = dto.Languages.Select(x => new AgentLanguage { LanguageCode = Enum.Parse<LanguageCode>(x) }).ToHashSet();
			var agent = new Agent(dto.Name, dto.MaxCapacity);
			agent.AddLanguages(agentLanguages);
			agent.AddSkills(skills);
			return agent;
		}
	}
}
