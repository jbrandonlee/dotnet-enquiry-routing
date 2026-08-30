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

		public async Task<Agent?> GetByIdAsync(Guid agentId)
		{
			return await dbContext.Agents
				.Include(x => x.Skills)
				.Include(x => x.Enquiries)
					.ThenInclude(e => e.Messages)
				.SingleOrDefaultAsync(x => x.Id == agentId);
		}

		public async Task<IEnumerable<Agent>> GetAllAsync()
		{
			return await dbContext.Agents.Include(x => x.Enquiries).ToListAsync();
		}

		public async Task<Agent?> GetActiveEnquiriesByAgentIdAsync(Guid agentId, DateTimeOffset dateTimeFrom)
		{
			return await dbContext.Agents
				.Include(x => x.Enquiries.Where(x => x.ClosedBy == null))
					.ThenInclude(e => e.Messages.Where(x => x.DateTimeCreated >= dateTimeFrom))
				.SingleOrDefaultAsync(x => x.Id == agentId);
		}

		public async Task<Agent?> GetByRequirementsAsync(LanguageCode languageCode, IEnumerable<Skill> requiredSkills, double matchingThreshold)
		{
			var requiredSkillIds = requiredSkills.Select(x => x.Id);
			IQueryable<Agent> queryable = dbContext.Agents.Include(x => x.Skills)
														  .Include(x => x.Enquiries);

			queryable = queryable.Where(x => x.Languages.Select(x => x.LanguageCode).Contains(languageCode))
								 .Where(x => (x.MaxCapacity - x.Enquiries.Count(e => e.ClosedBy == null)) > 0 && x.Status == AgentStatus.Online);
			
			// assign agent meets jaccard containment threshold
			queryable = queryable.Where(x => requiredSkillIds.Any() && requiredSkillIds.Count(skillId => x.Skills.Select(s => s.Id).Contains(skillId)) / (double)requiredSkills.Count() >= matchingThreshold);

			// assign agent that has lowest workload
			return await queryable.OrderByDescending(x => (x.MaxCapacity - x.Enquiries.Count(e => e.ClosedBy == null)) / (double) x.MaxCapacity)
								  .FirstOrDefaultAsync();
		}
	}
}
