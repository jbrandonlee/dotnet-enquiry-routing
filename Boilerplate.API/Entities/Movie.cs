namespace Boilerplate.API.Entities
{
	public class Movie(string title, int year)
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public string Title { get; set; } = title;
		public int Year { get; set; } = year;
	}
}
