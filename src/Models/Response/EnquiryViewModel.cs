using EnquiryRouting.Api.Entities;

namespace EnquiryRouting.Api.Models.Response
{
	public class EnquiryViewModel
	{
		public Guid EnquiryId { get; set; }
		public bool IsClosed { get; set; }
		public DateTimeOffset DateTimeCreated { get; set; }
		public IEnumerable<ChatMessageViewModel> Messages { get; set; } = new List<ChatMessageViewModel>();
	}

	public static class EnquiryViewModelExtensions
	{
		public static EnquiryViewModel ToViewModel(this Enquiry enquiry)
		{
			return new EnquiryViewModel
			{
				EnquiryId = enquiry.Id,
				IsClosed = enquiry.IsClosed,
				DateTimeCreated = enquiry.DateTimeCreated,
				Messages = enquiry.Messages.Select(x => x.ToViewModel()).OrderBy(x => x.DateTimeCreated)
			};
		}
	}
}
