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
			return await dbContext.Enquiries.FindAsync(enquiryId);
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
			return await dbContext.Enquiries
				.Where(x => x.IsPending)
				.Where(x => (double) agentSkills.Intersect(x.RequiredSkills).Count() / x.RequiredSkills.Count() > matchingThreshold)
				.OrderByDescending(x => x.IsUrgent) // IsUrgent comes first
				.ThenBy(x => x.DateTimeCreated) // LeastRecent comes first
				.Take(count)
				.ToListAsync();
		}
	}
}
