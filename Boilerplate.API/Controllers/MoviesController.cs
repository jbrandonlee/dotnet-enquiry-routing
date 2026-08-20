using Boilerplate.API.Entities;
using Boilerplate.API.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Boilerplate.API.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class MoviesController(ApplicationDbContext dbContext) : ControllerBase
	{
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Movie>>> Get()
		{
			return Ok(await dbContext.Movies.ToListAsync());
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Movie>> GetById(Guid id)
		{
			var movie = await dbContext.Movies.FindAsync(id);
			if (movie is null) return NotFound();

			return Ok(movie);
		}

		[HttpPost]
		public async Task<ActionResult<Movie>> Create([FromBody] CreateMovieRequest dto)
		{
			var movie = new Movie(dto.Title, dto.Year);
			
			await dbContext.Movies.AddAsync(movie);
			await dbContext.SaveChangesAsync();
			
			return CreatedAtAction(nameof(GetById), new { id = movie.Id }, movie);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMovieRequest dto)
		{
			var movie = await dbContext.Movies.FindAsync(id);
			if (movie is null) return NotFound();

			movie.Title = dto.Title ?? movie.Title;
			movie.Year = dto.Year ?? movie.Year;
			await dbContext.SaveChangesAsync();

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var movie = await dbContext.Movies.FindAsync(id);
			if (movie is null) return NotFound();

			dbContext.Movies.Remove(movie);
			await dbContext.SaveChangesAsync();

			return Ok();
		}
	}
}
