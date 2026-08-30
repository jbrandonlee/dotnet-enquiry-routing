using EnquiryRouting.Api.Entities;
using EnquiryRouting.Api.Enums;

namespace EnquiryRouting.Api.Interfaces
{
	public interface IAgentRepository
	{
		Task AddAsync(Agent agent);
		Task UpdateAsync(Agent agent);
		Task<IEnumerable<Agent>> GetAllAsync();
		Task<Agent?> GetByIdAsync(Guid agentId);
		Task<Agent?> GetActiveEnquiriesByAgentIdAsync(Guid agentId, DateTimeOffset dateTimeFrom);
		Task<Agent?> GetByRequirementsAsync(LanguageCode languageCode, IEnumerable<Skill> requiredSkills, double matchingThreshold);
	}
}
