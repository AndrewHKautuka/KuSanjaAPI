using Microsoft.OpenApi.Models;

namespace KuSanjaAPI;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.

		builder.Services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo { Title = "KuSanjaAPI", Version = "v1" });
		});
		builder.Services.AddCors(options =>
		{
			var frontendURL = builder.Configuration.GetValue<string>("FrontendURL");
			options.AddDefaultPolicy(builder =>
			{
				builder.WithOrigins(frontendURL).AllowAnyMethod().AllowAnyHeader();
			});
		});

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "KuSanjaAPI"));
		}

		app.UseHttpsRedirection();

		app.UseRouting();

		app.UseCors();

		app.UseAuthentication();

		app.UseAuthorization();

		app.MapControllers();

		app.Run();
	}
}
