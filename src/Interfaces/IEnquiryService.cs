using EnquiryRouting.Api.Entities;
using EnquiryRouting.Api.Models.Request;

namespace EnquiryRouting.Api.Interfaces
{
	public interface IEnquiryService
	{
		Task<Enquiry?> GetEnquiryByIdAsync(Guid enquiryId);
		Task<Enquiry?> GetRecentEnquiryMessagesByIdAsync(Guid enquiryId, DateTimeOffset dateTimeFrom);
		Task<Enquiry> CreateEnquiryAsync(SubmitEnquiryRequest dto);
		Task AddEnquiryMessageAsync(SubmitEnquiryMessageRequest dto);
		Task CloseEnquiryAsync(CloseEnquiryRequest dto);
	}
}
