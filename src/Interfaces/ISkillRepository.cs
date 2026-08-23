using EnquiryRouting.Api.Entities;

namespace EnquiryRouting.Api.Interfaces
{
	public interface ISkillRepository
	{
		Task<IEnumerable<Skill>> GetByNamesAsync(IEnumerable<string> names);
	}
}
