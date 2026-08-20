
using Boilerplate.API.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Boilerplate.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddDbContext<ApplicationDbContext>(
				options => options.UseNpgsql(builder.Configuration.GetConnectionString("Database"))
			);
			builder.Services.AddControllers();
			builder.Services.AddOpenApi();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.MapOpenApi();
				app.ApplyMigrations();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
