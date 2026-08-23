using EnquiryRouting.Api.Entities;
using EnquiryRouting.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EnquiryRouting.Api.Repositories
{
	public class EnquiryRepository(ApplicationDbContext dbContext) : IEnquiryRepository
	{
		public async Task AddAsync(Enquiry enquiry)
		{
			await dbContext.Enquiries.AddAsync(enquiry);
			await dbContext.SaveChangesAsync();
		}

		public async Task UpdateAsync(Enquiry enquiry)
		{
			dbContext.Enquiries.Update(enquiry);
			await dbContext.SaveChangesAsync();
		}

		public async Task<Enquiry?> GetByIdAsync(Guid enquiryId)
		{
			return await dbContext.Enquiries
				.Include(x => x.RequiredSkills)
				.Include(x => x.Messages)
				.SingleOrDefaultAsync(x => x.Id == enquiryId);
		}

		public async Task<IEnumerable<Enquiry>> GetByAgentIdAsync(Guid agentId)
		{
			return await dbContext.Enquiries
				.Include(x => x.Messages)
				.Where(x => x.AgentId == agentId)
				.ToListAsync();
		}

		public async Task<IEnumerable<Enquiry>> GetByClientIdAsync(Guid clientId)
		{
			return await dbContext.Enquiries
				.Include(x => x.Messages)
				.Where(x => x.CreatedBy == clientId)
				.ToListAsync();
		}

		public async Task<IEnumerable<Enquiry>> GetByClientIdAsync(Guid clientId, DateTimeOffset dateTimeLastReceived)
		{
			return await dbContext.Enquiries
				.Include(x => x.Messages.Where(m => m.DateTimeCreated > dateTimeLastReceived))
				.Where(x => x.CreatedBy == clientId)
				.ToListAsync();
		}

		public async Task<IEnumerable<Enquiry>> GetByRequirementsAsync(int count, IEnumerable<Skill> agentSkills, double matchingThreshold)
		{
			var agentSkillIds = agentSkills.Select(x => x.Id);
			return await dbContext.Enquiries
				.Include(x => x.RequiredSkills)
				.Include(x => x.Messages)
				.Where(x => x.AgentId == null && x.ClosedBy == null)
				.Where(x => x.RequiredSkills.Any() && x.RequiredSkills.Count(requiredSkill => agentSkillIds.Contains(requiredSkill.Id)) / (double) x.RequiredSkills.Count() >= matchingThreshold)
				.OrderByDescending(x => x.RequiredSkills.Any(x => x.IsPriority)) // IsUrgent comes first
				.ThenBy(x => x.DateTimeCreated) // LeastRecent comes first
				.Take(count)
				.ToListAsync();
		}
	}
}
