using LuxuryPaintJohnsonAPI.Models;

namespace LuxuryPaintJohnsonAPI.Repositories;

public interface IProjectRepository
{
	Task<IEnumerable<Project>> GetProjectsAsync();

	Task<Project> GetProjectByIdAsync(int id);

	Task<Project> AddProjectAsync(Project project);

	Task<Project> UpdateProjectAsync(Project project);

	Task DeleteProjectAsync(int id);
}
