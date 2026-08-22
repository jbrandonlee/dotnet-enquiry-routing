using EnquiryRouting.Api.Entities;
using EnquiryRouting.Api.Models.Request;

namespace EnquiryRouting.Api.Interfaces
{
	public interface IAgentService
	{
		Task<Agent?> GetAgentByIdAsync(Guid agentId);
		Task<Agent> CreateAgentAsync(CreateAgentRequest dto);
		Task UpdateAgentStatusAsync(UpdateAgentStatusRequest dto);
	}
}
