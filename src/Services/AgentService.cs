using EnquiryRouting.Api.Entities;
using EnquiryRouting.Api.Enums;
using EnquiryRouting.Api.Interfaces;
using EnquiryRouting.Api.Models.Request;

namespace EnquiryRouting.Api.Services
{
	public class AgentService(IAgentRepository agentRepository, ISkillRepository skillRepository, IMatchingService matchingService) : IAgentService
	{
		public async Task<Agent?> GetAgentByIdAsync(Guid agentId)
		{
			return await agentRepository.GetByIdAsync(agentId);
		}

		public async Task<IEnumerable<Agent>> GetAllAgentsAsync()
		{
			return await agentRepository.GetAllAsync();
		}

		public async Task<Agent?> GetActiveEnquiriesByAgentIdAsync(Guid agentId, DateTimeOffset dateTimeFrom)
		{
			return await agentRepository.GetActiveEnquiriesByAgentIdAsync(agentId, dateTimeFrom);
		}

		public async Task<Agent> CreateAgentAsync(CreateAgentRequest dto)
		{
			var skills = await skillRepository.GetByNamesAsync(dto.Skills);
			var agent = dto.ToDomainModel(skills.ToHashSet());

			await agentRepository.AddAsync(agent);
			return agent;
		}

		public async Task CreateAgentsAsync(CreateAgentsRequest dto)
		{
			foreach (var agentDto in dto.Agents)
			{
				await CreateAgentAsync(agentDto);
			}
		}

		public async Task UpdateAgentStatusAsync(UpdateAgentStatusRequest dto)
		{
			var agent = await agentRepository.GetByIdAsync(dto.AgentId);
			if (agent is null) return;

			agent.UpdateStatus(dto.Status);
			await agentRepository.UpdateAsync(agent);

			if (dto.Status == AgentStatus.Online.ToString())
			{
				await matchingService.TryMatchRecentEnquiriesAsync(agent);
			}
		}
	}
}
