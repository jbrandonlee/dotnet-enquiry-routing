using EnquiryRouting.Api.Entities;

namespace EnquiryRouting.Api.Interfaces
{
	public interface IEnquiryRepository
	{
		Task AddAsync(Enquiry enquiry);
		Task UpdateAsync(Enquiry enquiry);
		Task<Enquiry?> GetByIdAsync(Guid enquiryId);
		Task<IEnumerable<Enquiry>> GetByAgentIdAsync(Guid agentId);
		Task<IEnumerable<Enquiry>> GetByClientIdAsync(Guid clientId);
		Task<IEnumerable<Enquiry>> GetByClientIdAsync(Guid clientId, DateTimeOffset dateTimeLastReceived);
	}
}
