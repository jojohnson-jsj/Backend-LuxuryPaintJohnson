using Azure.Identity;
using LuxuryPaintJohnsonAPI.Data;
using LuxuryPaintJohnsonAPI.Repositories;
using LuxuryPaintJohnsonAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

public class Startup
{
	public Startup(IConfiguration configuration)
	{
		Configuration = configuration;
	}

	public IConfiguration Configuration { get; }

	public void ConfigureServices(IServiceCollection services)
	{
		var azureCredential = new DefaultAzureCredential();

		services.AddDbContext<AppDbContext>(options =>
		{
			var connectionString = Configuration.GetConnectionString("DefaultConnection");
			options.UseSqlServer(connectionString, sqlOptions =>
			{
				sqlOptions.EnableRetryOnFailure();
			});
		});

		services.AddScoped<IPhotoService, PhotoService>();
		services.AddScoped<IPhotoRepository, PhotoRepository>();
		services.AddScoped<IProjectService, ProjectService>();
		services.AddScoped<IProjectRepository, ProjectRepository>();

		services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
		services.AddControllers();
		services.AddCors(options =>
		{
			options.AddPolicy("AllowSpecificOrigins", policy =>
			{
				policy.WithOrigins("http://localhost:5173")
					  .AllowAnyHeader()
					  .AllowAnyMethod();
			});
		});
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "LuxuryPaintJohnsonAPI",
				Version = "v1"
			});
		});
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment() || env.IsProduction())
		{
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "LuxuryPaintJohnsonAPI V1");
				c.RoutePrefix = string.Empty;
			});
		}

		app.UseStaticFiles();
		app.UseHttpsRedirection();
		app.UseRouting();
		app.UseAuthorization();

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllers(); // Correctly maps controller routes
		});
	}
}
