using EnquiryRouting.Api.Entities;
using EnquiryRouting.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EnquiryRouting.Api.Repositories
{
	public class SkillRepository(ApplicationDbContext dbContext) : ISkillRepository
	{
		public async Task<IEnumerable<Skill>> GetByNamesAsync(IEnumerable<string> names)
		{
			return await dbContext.Skills.Where(x => names.Contains(x.Name)).ToListAsync();
		}
	}
}
