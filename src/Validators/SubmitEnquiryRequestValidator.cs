using EnquiryRouting.Api.Models.Request;
using FluentValidation;

namespace EnquiryRouting.Api.Validators
{
	public class SubmitEnquiryRequestValidator : AbstractValidator<SubmitEnquiryRequest>
	{
		public SubmitEnquiryRequestValidator()
		{
			RuleFor(x => x.RequiredSkills)
				.NotEmpty()
				.Must(skills => skills.Count() <= 2);
		}
	}
}
