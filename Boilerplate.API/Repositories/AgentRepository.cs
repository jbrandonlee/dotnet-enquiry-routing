using EnquiryRouting.Api.Entities;
using EnquiryRouting.Api.Enums;
using EnquiryRouting.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

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

		public async Task<Agent?> GetByIdAsync(Guid agentId) // TODO: Include Enquiries? Messages?
		{
			return await dbContext.Agents.FindAsync(agentId);
		}

		public async Task<Agent?> GetByRequirementsAsync(LanguageCode languageCode, IEnumerable<Skill> requiredSkills, double matchingThreshold)
		{
			var queryable = dbContext.Agents.Where(x => x.Languages.Contains(languageCode))
											.Where(x => x.IsAvailable);
			
			// assign agent meets jaccard containment threshold
			queryable = queryable.Where(x => (double) x.Skills.Intersect(requiredSkills).Count() / requiredSkills.Count() > matchingThreshold);

			// assign agent that has lowest workload
			return await queryable.OrderByDescending(x => (double) x.RemainingCapacity / x.MaxCapacity).FirstOrDefaultAsync();
		}
	}
}
