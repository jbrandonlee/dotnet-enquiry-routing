using EnquiryRouting.Api.Extensions;
using EnquiryRouting.Api.Interfaces;
using EnquiryRouting.Api.Models.Request;
using EnquiryRouting.Api.Repositories;
using EnquiryRouting.Api.Services;
using EnquiryRouting.Api.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EnquiryRouting.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddOpenApi();
			builder.Services.AddControllers();
			builder.Services.AddDbContext<ApplicationDbContext>(
				options => options.UseNpgsql(builder.Configuration.GetConnectionString("Database"))
			);

			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowAll", policy =>
				{
					policy
						.AllowAnyOrigin()
						.AllowAnyHeader()
						.AllowAnyMethod();
				});
			});

			#region Add FluentValidation
			builder.Services.AddTransient<IValidator<SubmitEnquiryRequest>, SubmitEnquiryRequestValidator>();
			#endregion

			#region Add Domain Services
			builder.Services.AddScoped<IAgentService, AgentService>();
			builder.Services.AddScoped<IEnquiryService, EnquiryService>();
			builder.Services.AddScoped<IMatchingService, MatchingService>();
			#endregion

			#region Add Repositories
			builder.Services.AddScoped<IAgentRepository, AgentRepository>();
			builder.Services.AddScoped<IEnquiryRepository, EnquiryRepository>();
			builder.Services.AddScoped<ISkillRepository, SkillRepository>();
			#endregion

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.MapOpenApi();
				app.ApplyMigrations();
			}

			app.UseHttpsRedirection();
			app.UseAuthorization();
			app.UseCors("AllowAll");
			app.MapControllers();

			app.Run();
		}
	}
}
