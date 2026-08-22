using EnquiryRouting.Api.Entities;

namespace EnquiryRouting.Api.Interfaces
{
	public interface IAgentRepository
	{
		Task AddAsync(Agent agent);
		Task UpdateAsync(Agent agent);
		Task<Agent?> GetByIdAsync(Guid agentId);
	}
}
