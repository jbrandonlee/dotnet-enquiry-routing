using EnquiryRouting.Api.Entities;
using EnquiryRouting.Api.Interfaces;
using EnquiryRouting.Api.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace EnquiryRouting.Api.Controllers
{
	public class AgentsController(IAgentService agentService) : ControllerBase
	{
		[HttpGet("{id}")]
		public async Task<ActionResult<Agent>> GetById(Guid id, [FromQuery] DateTimeOffset? after)
		{
			var agent = await agentService.GetAgentByIdAsync(id);
			if (agent is null) return NotFound();
			return Ok(agent);
		}

		[HttpPost]
		public async Task<ActionResult<Agent>> CreateAgent([FromBody] CreateAgentRequest dto)
		{
			var agent = await agentService.CreateAgentAsync(dto);
			return CreatedAtAction(nameof(GetById), new { id = agent.Id }, agent);
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
