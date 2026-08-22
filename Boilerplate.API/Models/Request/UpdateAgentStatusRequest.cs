namespace EnquiryRouting.Api.Models.Request
{
	public class UpdateAgentStatusRequest
	{
		public Guid AgentId { get; set; }
		public string Status { get; set; }
	}
}
