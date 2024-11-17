using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using LuxuryPaintJohnsonAPI.Models;
using LuxuryPaintJohnsonAPI.Models.Dtos;
using LuxuryPaintJohnsonAPI.Services;

namespace LuxuryPaintJohnsonAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhotosController : ControllerBase
{
	private readonly IPhotoService photoService;
	private readonly IMapper mapper;

	public PhotosController(IPhotoService photoService, IMapper mapper)
	{
		this.photoService = photoService;
		this.mapper = mapper;
	}

	[HttpGet]
	public async Task<IActionResult> GetPhotos([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
	{
		if (page <= 0 || pageSize <= 0)
		{
			return BadRequest("Page and pageSize must be greater than zero.");
		}

		var photos = await this.photoService.GetPhotosAsync();

		var paginatedPhotos = photos
			.Skip((page - 1) * pageSize)
			.Take(pageSize)
			.ToList();

		var paginatedPhotosDto = this.mapper.Map<IEnumerable<PhotoDto>>(paginatedPhotos);

		return Ok(new
		{
			Data = paginatedPhotosDto,
			TotalCount = photos.Count(),
			Page = page,
			PageSize = pageSize
		});
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetPhoto(int id)
	{
		var photo = await this.photoService.GetPhotoByIdAsync(id);

		if (photo is null)
		{
			return NotFound($"Photo with ID {id} not found.");
		}

		var photoDto = this.mapper.Map<PhotoDto>(photo);
		return Ok(photoDto);
	}

	[HttpPost]
	public async Task<IActionResult> AddPhoto([FromBody] PhotoDto photoDto)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var photo = this.mapper.Map<Photo>(photoDto);
		var createdPhoto = await this.photoService.AddPhotoAsync(photo);

		var createdPhotoDto = this.mapper.Map<PhotoDto>(createdPhoto);

		return CreatedAtAction(nameof(GetPhoto), new { id = createdPhotoDto.Id }, createdPhotoDto);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdatePhoto(int id, [FromBody] PhotoDto photoDto)
	{
		if (id != photoDto.Id || !ModelState.IsValid)
		{
			return BadRequest();
		}

		var existingPhoto = await this.photoService.GetPhotoByIdAsync(id);
		if (existingPhoto is null)
		{
			return NotFound($"Photo with ID {id} not found.");
		}

		var photo = this.mapper.Map<Photo>(photoDto);
		var updatedPhoto = await this.photoService.UpdatePhotoAsync(photo);

		var updatedPhotoDto = this.mapper.Map<PhotoDto>(updatedPhoto);

		return Ok(updatedPhotoDto);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeletePhoto(int id)
	{
		var existingPhoto = await this.photoService.GetPhotoByIdAsync(id);
		if (existingPhoto is null)
		{
			return NotFound($"Photo with ID {id} not found.");
		}

		await this.photoService.DeletePhotoAsync(id);

		return NoContent();
	}
}
