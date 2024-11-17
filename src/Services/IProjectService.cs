using LuxuryPaintJohnsonAPI.Models;

namespace LuxuryPaintJohnsonAPI.Services;

public interface IProjectService
{
	Task<IEnumerable<Project>> GetProjectsAsync();

	Task<Project> GetProjectByIdAsync(int id);

	Task<Project> AddProjectAsync(Project photo);

	Task<Project> UpdateProjectAsync(Project photo);

	Task DeleteProjectAsync(int id);
}
