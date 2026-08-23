using EnquiryRouting.Api.Entities;
using EnquiryRouting.Api.Interfaces;

namespace EnquiryRouting.Api.Services
{
	public class MatchingService(IAgentRepository agentRepository, IEnquiryRepository enquiryRepository) : IMatchingService
	{
		private readonly double _matchingThreshold = 0.50;

		public async Task TryMatchEnquiryAsync(Enquiry enquiry)
		{
			var agent = await agentRepository.GetByRequirementsAsync(enquiry.LanguageCode, enquiry.RequiredSkills, _matchingThreshold);
			if (agent is null) return;

			agent.AssignEnquiry(enquiry);
			await agentRepository.UpdateAsync(agent);
		}

		public async Task TryMatchRecentEnquiriesAsync(Agent agent)
		{
			if (agent is null || !agent.IsAvailable) return;

			var matchingEnquiries = await enquiryRepository.GetByRequirementsAsync(agent.RemainingCapacity, agent.Skills, _matchingThreshold);

			if (!matchingEnquiries.Any()) return;
			
			foreach (var enquiry in matchingEnquiries)
			{
				agent.AssignEnquiry(enquiry);
			}

			await agentRepository.UpdateAsync(agent);
		}

		public async Task TryMatchRecentEnquiriesAsync(Guid agentId)
		{
			var agent = await agentRepository.GetByIdAsync(agentId);
			if (agent is null) return;

			await TryMatchRecentEnquiriesAsync(agent);
		}
	}
}
