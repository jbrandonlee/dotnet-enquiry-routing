using EnquiryRouting.Api.Interfaces;
using EnquiryRouting.Api.Models.Request;
using EnquiryRouting.Api.Models.Response;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EnquiryRouting.Api.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class EnquiriesController(IEnquiryService enquiryService, IValidator<SubmitEnquiryRequest> submitEnquiryRequestValidator) : ControllerBase
	{
		[HttpGet("{id}")]
		public async Task<ActionResult<EnquiryViewModel>> GetById(Guid id, [FromQuery] DateTimeOffset? from)
		{
			var dateTimeFrom = from ?? DateTimeOffset.MinValue;
			var enquiry = await enquiryService.GetRecentEnquiryMessagesByIdAsync(id, dateTimeFrom);
			if (enquiry is null) return NotFound();
			return Ok(enquiry.ToViewModel());
		}

		[HttpPost]
		public async Task<ActionResult<EnquiryViewModel>> SubmitEnquiry([FromBody] SubmitEnquiryRequest dto)
		{
			if (!submitEnquiryRequestValidator.Validate(dto).IsValid) return BadRequest();
			var enquiry = await enquiryService.CreateEnquiryAsync(dto);
			return CreatedAtAction(nameof(GetById), new { id = enquiry.Id }, enquiry.ToViewModel());
		}

		[HttpPost("{id}/message")]
		public async Task<IActionResult> SubmitEnquiryMessage(Guid id, [FromBody] SubmitEnquiryMessageRequest dto)
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
