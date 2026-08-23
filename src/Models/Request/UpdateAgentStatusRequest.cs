namespace EnquiryRouting.Api.Models.Request
{
	public class UpdateAgentStatusRequest
	{
		public Guid AgentId { get; set; }
		public required string Status { get; set; }
	}
}
