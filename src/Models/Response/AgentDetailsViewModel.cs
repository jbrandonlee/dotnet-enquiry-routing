using EnquiryRouting.Api.Entities;
using EnquiryRouting.Api.Enums;

namespace EnquiryRouting.Api.Models.Response
{
	public class AgentDetailsViewModel
	{
		public Guid Id { get; set; }
		public string AgentName { get; set; } = string.Empty;
		public AgentStatus Status { get; set; }
		public int ActiveEnquiriesCount { get; set; }
		public int MaxCapacity { get; set; }
	}

	public static class AgentDetailsViewModelExtensions
	{
		public static AgentDetailsViewModel ToDetailsViewModel(this Agent agent)
		{
			return new AgentDetailsViewModel
			{
				Id = agent.Id,
				AgentName = agent.Name,
				Status = agent.Status,
				ActiveEnquiriesCount = agent.Enquiries.Count(x => !x.IsClosed),
				MaxCapacity = agent.MaxCapacity
			};
		}
	}
}
