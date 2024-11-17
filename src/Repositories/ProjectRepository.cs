using LuxuryPaintJohnsonAPI.Data;
using LuxuryPaintJohnsonAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LuxuryPaintJohnsonAPI.Repositories;

public class ProjectRepository : IProjectRepository
{
	private readonly AppDbContext context;
	
	public ProjectRepository(AppDbContext context)
	{
		this.context = context;
	}

	public async Task<IEnumerable<Project>> GetProjectsAsync()
	{
		return await this.context.Projects
			.Include(p => p.Photos) // Eagerly load the photos
			.ToListAsync();
	}

	public async Task<Project> GetProjectByIdAsync(int id)
	{
		return await this.context.Projects
			.Include(p => p.Photos) // Eagerly load related Photos
			.FirstOrDefaultAsync(p => p.Id == id) ?? new Project
			{
				Id = 0,
				Title = "Default Project",
				Photos = new List<Photo>(),
				CreatedAt = DateTime.MinValue
			};
	}

	public async Task<Project> AddProjectAsync(Project project)
	{
		await this.context.Projects.AddAsync(project);
		await context.SaveChangesAsync();

		return project;
	}

	public async Task<Project> UpdateProjectAsync(Project project)
	{
		this.context.Projects.Update(project);
		await this.context.SaveChangesAsync();

		return project;
	}

	public async Task DeleteProjectAsync(int id)
	{
		var project = await this.context.Projects.FindAsync(id);
		
		if (project is not null)
		{
			this.context.Projects.Remove(project);
			await this.context.SaveChangesAsync();
		}
	}
}
