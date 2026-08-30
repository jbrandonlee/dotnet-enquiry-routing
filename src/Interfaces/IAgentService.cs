using EnquiryRouting.Api.Entities;
using EnquiryRouting.Api.Models.Request;

namespace EnquiryRouting.Api.Interfaces
{
	public interface IAgentService
	{
		Task<Agent?> GetAgentByIdAsync(Guid agentId);
		Task<IEnumerable<Agent>> GetAllAgentsAsync();
		Task<Agent?> GetActiveEnquiriesByAgentIdAsync(Guid agentId, DateTimeOffset dateTimeFrom);
		Task<Agent> CreateAgentAsync(CreateAgentRequest dto);
		Task CreateAgentsAsync(CreateAgentsRequest dto);
		Task UpdateAgentStatusAsync(UpdateAgentStatusRequest dto);
	}
}
