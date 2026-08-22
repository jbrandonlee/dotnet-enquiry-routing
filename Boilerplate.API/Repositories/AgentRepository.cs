using EnquiryRouting.Api.Entities;
using EnquiryRouting.Api.Interfaces;

namespace EnquiryRouting.Api.Repositories
{
	public class AgentRepository(ApplicationDbContext dbContext) : IAgentRepository
	{
		public async Task AddAsync(Agent agent)
		{
			await dbContext.Agents.AddAsync(agent);
			await dbContext.SaveChangesAsync();
		}

		public async Task UpdateAsync(Agent agent)
		{
			dbContext.Agents.Update(agent);
			await dbContext.SaveChangesAsync();
		}

		public async Task<Agent?> GetByIdAsync(Guid agentId)
		{
			return await dbContext.Agents.FindAsync(agentId);
		}
	}
}
