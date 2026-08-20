namespace Boilerplate.API.Models.Request
{
	public class CreateMovieRequest
	{
		public string Title { get; set; } = string.Empty;
		public int Year { get; set; }
	}
}
