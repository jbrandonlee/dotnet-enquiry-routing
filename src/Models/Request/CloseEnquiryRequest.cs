namespace EnquiryRouting.Api.Models.Request
{
	public class CloseEnquiryRequest
	{
		public Guid EnquiryId { get; set; }
		public Guid AgentId { get; set; }
	}
}
