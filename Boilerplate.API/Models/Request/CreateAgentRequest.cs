using EnquiryRouting.Api.Entities;
using EnquiryRouting.Api.Enums;

namespace EnquiryRouting.Api.Models.Request
{
	public class CreateAgentRequest
	{
		public string Name = string.Empty;
		public int Capacity;
		public IEnumerable<string> Languages = new List<string>();
		public IEnumerable<string> Skills = new List<string>();
	}

	public static class CreateAgentRequestExtensions
	{
		public static Agent ToDomainModel(this CreateAgentRequest dto, HashSet<Skill> skills)
		{
			var languageCodes = dto.Languages.Select(x => Enum.Parse<LanguageCode>(x)).ToHashSet();
			var agent = new Agent(dto.Name, dto.Capacity, languageCodes, skills);
			return agent;
		}
	}
}
