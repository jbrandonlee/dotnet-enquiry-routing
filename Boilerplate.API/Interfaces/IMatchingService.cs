using EnquiryRouting.Api.Entities;

namespace EnquiryRouting.Api.Interfaces
{
	public interface IMatchingService
	{
		Task TryMatchEnquiryAsync(Enquiry enquiry);
		Task TryMatchRecentEnquiriesAsync(Agent agent);
		Task TryMatchRecentEnquiriesAsync(Guid agentId);
	}
}
