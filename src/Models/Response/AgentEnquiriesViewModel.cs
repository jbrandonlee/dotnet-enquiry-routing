using EnquiryRouting.Api.Entities;

namespace EnquiryRouting.Api.Models.Response
{
	public class AgentEnquiriesViewModel
	{
		public string AgentName { get; set; } = string.Empty;
		public IEnumerable<EnquiryViewModel> Enquiries { get; set; } = new List<EnquiryViewModel>();
	}

	public static class AgentEnquiriesViewModelExtensions
	{
		public static AgentEnquiriesViewModel ToEnquiriesViewModel(this Agent agent)
		{
			return new AgentEnquiriesViewModel
			{
				AgentName = agent.Name,
				Enquiries = agent.Enquiries.Select(x => x.ToViewModel())
										   .OrderBy(x => x.DateTimeCreated)
			};
		}
	}
}
