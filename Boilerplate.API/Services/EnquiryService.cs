using EnquiryRouting.Api.Entities;
using EnquiryRouting.Api.Interfaces;
using EnquiryRouting.Api.Models.Request;

namespace EnquiryRouting.Api.Services
{
	public class EnquiryService(IEnquiryRepository enquiryRepository, ISkillRepository skillRepository) : IEnquiryService
	{
		public async Task<Enquiry?> GetEnquiryByIdAsync(Guid enquiryId)
		{
			return await enquiryRepository.GetByIdAsync(enquiryId);
		}

		public async Task<IEnumerable<Enquiry>> GetEnquiriesByAgentIdAsync(Guid agentId)
		{
			return await enquiryRepository.GetByAgentIdAsync(agentId);
		}

		public async Task<Enquiry> CreateEnquiryAsync(SubmitEnquiryRequest dto)
		{
			var skills = await skillRepository.GetByNamesAsync(dto.RequiredSkills);
			var enquiry = dto.ToDomainModel(skills.ToHashSet());

			await enquiryRepository.AddAsync(enquiry);
			return enquiry;
		}

		public async Task AddEnquiryMessageAsync(SubmitEnquiryMessageRequest dto)
		{
			var enquiry = await enquiryRepository.GetByIdAsync(dto.EnquiryId);
			if (enquiry is null) return;

			var chatMessage = dto.ToDomainModel();
			enquiry.AddMessage(chatMessage);
			await enquiryRepository.UpdateAsync(enquiry);
		}

		public async Task CloseEnquiryAsync(CloseEnquiryRequest dto)
		{
			var enquiry = await enquiryRepository.GetByIdAsync(dto.EnquiryId);
			if (enquiry is null) return;

			enquiry.Close(dto.AgentId);
			await enquiryRepository.UpdateAsync(enquiry);
		}
	}
}
