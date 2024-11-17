using Microsoft.AspNetCore.Mvc;
using LuxuryPaintJohnsonAPI.Models;
using LuxuryPaintJohnsonAPI.Services;
using AutoMapper;
using LuxuryPaintJohnsonAPI.Models.Dtos;

namespace LuxuryPaintJohnsonAPI.Controllers;

[ApiController]
[Route("api/projects")]
public class ProjectsController : ControllerBase
{
	private readonly IProjectService projectService;
	private readonly IMapper mapper;

	public ProjectsController(IProjectService projectService, IMapper mapper)
	{
		this.projectService = projectService;
		this.mapper = mapper;
	}

	[HttpGet]
	public async Task<IActionResult> GetProjects()
	{
		var projects = await this.projectService.GetProjectsAsync();
		var projectsDto = this.mapper.Map<IEnumerable<ProjectDto>>(projects);

		return Ok(projectsDto);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetProject(int id)
	{
		var project = await this.projectService.GetProjectByIdAsync(id);

		if (project == null)
		{
			return NotFound($"Project with ID {id} not found.");
		}

		var projectDto = this.mapper.Map<ProjectDto>(project);

		return Ok(projectDto);
	}

	[HttpGet("{id}/photos")]
	public async Task<IActionResult> GetPhotosForProject(int id)
	{
		var project = await this.projectService.GetProjectByIdAsync(id);

		if (project == null || project.Photos == null)
		{
			return NotFound($"Photos for Project with ID {id} not found.");
		}

		var photosDto = this.mapper.Map<IEnumerable<PhotoDto>>(project.Photos);

		return Ok(photosDto);
	}

	[HttpPost]
	public async Task<IActionResult> AddProject([FromBody] ProjectDto projectDto)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var project = this.mapper.Map<Project>(projectDto);
		var createdProject = await this.projectService.AddProjectAsync(project);
		var createdProjectDto = this.mapper.Map<ProjectDto>(createdProject);

		return CreatedAtAction(nameof(GetProject), new { id = createdProjectDto.Id }, createdProjectDto);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateProject(int id, [FromBody] ProjectDto projectDto)
	{
		if (id != projectDto.Id || !ModelState.IsValid)
		{
			return BadRequest();
		}

		var existingProject = await projectService.GetProjectByIdAsync(id);

		if (existingProject == null)
		{
			return NotFound($"Project with ID {id} not found.");
		}

		var project = this.mapper.Map<Project>(projectDto);
		project.Id = id;

		var updatedProject = await this.projectService.UpdateProjectAsync(project);
		var updatedProjectDto = this.mapper.Map<ProjectDto>(updatedProject);

		return Ok(updatedProjectDto);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteProject(int id)
	{
		var existingProject = await projectService.GetProjectByIdAsync(id);
		if (existingProject == null)
		{
			return NotFound($"Project with ID {id} not found.");
		}

		await this.projectService.DeleteProjectAsync(id);

		return NoContent();
	}
}
