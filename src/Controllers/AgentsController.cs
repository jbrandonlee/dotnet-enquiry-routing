using EnquiryRouting.Api.Entities;
using EnquiryRouting.Api.Interfaces;
using EnquiryRouting.Api.Models.Request;
using EnquiryRouting.Api.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace EnquiryRouting.Api.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AgentsController(IAgentService agentService) : ControllerBase
	{
		[HttpGet("{id}")]
		public async Task<ActionResult<Agent>> GetById(Guid id)
		{
			var agent = await agentService.GetAgentByIdAsync(id);
			if (agent is null) return NotFound();
			return Ok(agent);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<AgentDetailsViewModel>>> GetAll()
		{
			var agentList = await agentService.GetAllAgentsAsync();
			var viewModel = agentList.Select(x => x.ToDetailsViewModel()).OrderBy(x => x.AgentName);
			return Ok(viewModel);
		}

		[HttpGet("{id}/enquiries")]
		public async Task<ActionResult<AgentEnquiriesViewModel>> GetEnquiriesById(Guid id, [FromQuery] long? from)
		{
			var dateTimeFrom = (from is not null) ? DateTimeOffset.FromUnixTimeSeconds(from.Value) : DateTimeOffset.MinValue;
			var agent = await agentService.GetActiveEnquiriesByAgentIdAsync(id, dateTimeFrom);
			if (agent is null) return NotFound();
			return Ok(agent.ToEnquiriesViewModel());
		}

		[HttpPost]
		public async Task<IActionResult> CreateAgents([FromBody] CreateAgentsRequest dto)
		{
			await agentService.CreateAgentsAsync(dto);
			return Ok();
		}

		[HttpPut("{id}/status")]
		public async Task<IActionResult> UpdateAgentStatus(Guid id, [FromBody] UpdateAgentStatusRequest dto)
		{
			if (id != dto.AgentId) return BadRequest();
			await agentService.UpdateAgentStatusAsync(dto);
			return Ok();
		}
	}
}
