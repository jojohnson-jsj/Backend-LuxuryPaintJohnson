using LuxuryPaintJohnsonAPI.Models;
using LuxuryPaintJohnsonAPI.Repositories;

namespace LuxuryPaintJohnsonAPI.Services;

public class PhotoService : IPhotoService
{
	private readonly IPhotoRepository photoRepository;

	public PhotoService(IPhotoRepository photoRepository)
	{
		this.photoRepository = photoRepository;
	}

	public async Task<IEnumerable<Photo>> GetPhotosAsync()
	{
		return await this.photoRepository.GetPhotosAsync();
	}

	public async Task<Photo> GetPhotoByIdAsync(int id)
	{
		return await this.photoRepository.GetPhotoByIdAsync(id);
	}

	public async Task<Photo> AddPhotoAsync(Photo photo)
	{
		photo.CreatedAt = DateTime.UtcNow;
		return await this.photoRepository.AddPhotoAsync(photo);
	}

	public async Task<Photo> UpdatePhotoAsync(Photo photo)
	{
		return await this.photoRepository.UpdatePhotoAsync(photo);
	}

	public async Task DeletePhotoAsync(int id)
	{
		await this.photoRepository.DeletePhotoAsync(id);
	}
}
