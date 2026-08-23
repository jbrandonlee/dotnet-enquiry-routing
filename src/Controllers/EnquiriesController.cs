using EnquiryRouting.Api.Entities;
using EnquiryRouting.Api.Interfaces;
using EnquiryRouting.Api.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace EnquiryRouting.Api.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class EnquiriesController(IEnquiryService enquiryService) : ControllerBase
	{
		[HttpGet("{id}")]
		public async Task<ActionResult<Enquiry>> GetById(Guid id, [FromQuery] DateTimeOffset? after)
		{
			var enquiry = await enquiryService.GetEnquiryByIdAsync(id);
			if (enquiry is null) return NotFound();
			return Ok(enquiry);
		}

		[HttpPost]
		public async Task<ActionResult<Enquiry>> SubmitEnquiry([FromBody] SubmitEnquiryRequest dto)
		{
			var enquiry = await enquiryService.CreateEnquiryAsync(dto);
			return CreatedAtAction(nameof(GetById), new { id = enquiry.Id }, enquiry);
		}

		[HttpPost("{id}/message")]
		public async Task<ActionResult<ChatMessage>> SubmitEnquiryMessage(Guid id, [FromBody] SubmitEnquiryMessageRequest dto)
		{
			if (id != dto.EnquiryId) return BadRequest();
			await enquiryService.AddEnquiryMessageAsync(dto);
			return Ok();
		}

		[HttpPut("{id}/close")]
		public async Task<IActionResult> CloseEnquiry(Guid id, [FromBody] CloseEnquiryRequest dto)
		{
			if (id != dto.EnquiryId) return BadRequest();
			await enquiryService.CloseEnquiryAsync(dto);
			return Ok();
		}
	}
}
