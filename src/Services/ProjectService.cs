using LuxuryPaintJohnsonAPI.Models;
using LuxuryPaintJohnsonAPI.Repositories;

namespace LuxuryPaintJohnsonAPI.Services;

public class ProjectService : IProjectService
{
	private readonly IProjectRepository projectRepository;

	public ProjectService(IProjectRepository projectRepository)
	{
		this.projectRepository = projectRepository;
	}

	public async Task<IEnumerable<Project>> GetProjectsAsync()
	{
		return await this.projectRepository.GetProjectsAsync();
	}

	public async Task<Project> GetProjectByIdAsync(int id)
	{
		return await this.projectRepository.GetProjectByIdAsync(id);
	}

	public async Task<Project> AddProjectAsync(Project project)
	{
		project.CreatedAt = DateTime.UtcNow;
		return await this.projectRepository.AddProjectAsync(project);
	}

	public async Task<Project> UpdateProjectAsync(Project project)
	{
		return await this.projectRepository.UpdateProjectAsync(project);
	}

	public async Task DeleteProjectAsync(int id)
	{
		await this.projectRepository.DeleteProjectAsync(id);
	}
}
