using EnquiryRouting.Api.Entities;
using EnquiryRouting.Api.Enums;

namespace EnquiryRouting.Api.Models.Request
{
	public class CreateAgentRequest
	{
		public required string Name;
		public int MaxCapacity;
		public IEnumerable<string> Languages = new List<string>();
		public IEnumerable<string> Skills = new List<string>();
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
