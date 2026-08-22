using EnquiryRouting.Api.Entities;
using EnquiryRouting.Api.Interfaces;
using EnquiryRouting.Api.Models.Request;

namespace EnquiryRouting.Api.Services
{
	public class AgentService(IAgentRepository agentRepository, ISkillRepository skillRepository) : IAgentService
	{
		public async Task<Agent?> GetAgentByIdAsync(Guid agentId)
		{
			return await agentRepository.GetByIdAsync(agentId);
		}

		public async Task<Agent> CreateAgentAsync(CreateAgentRequest dto)
		{
			var skills = await skillRepository.GetByNamesAsync(dto.Skills);
			var agent = dto.ToDomainModel(skills.ToHashSet());

			await agentRepository.AddAsync(agent);
			return agent;
		}

		public async Task UpdateAgentStatusAsync(UpdateAgentStatusRequest dto)
		{
			var agent = await agentRepository.GetByIdAsync(dto.AgentId);
			if (agent is null) return;

			agent.UpdateStatus(dto.Status);
			await agentRepository.UpdateAsync(agent);
		}
	}
}
