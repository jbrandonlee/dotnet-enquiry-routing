using EnquiryRouting.Api.Entities;
using EnquiryRouting.Api.Enums;

namespace EnquiryRouting.Api.Interfaces
{
	public interface IAgentRepository
	{
		Task AddAsync(Agent agent);
		Task UpdateAsync(Agent agent);
		Task<Agent?> GetByIdAsync(Guid agentId);
		Task<Agent?> GetByRequirementsAsync(LanguageCode languageCode, IEnumerable<Skill> requiredSkills, double matchingThreshold);
	}
}
