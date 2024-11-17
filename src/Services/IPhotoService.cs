using LuxuryPaintJohnsonAPI.Models;

namespace LuxuryPaintJohnsonAPI.Services;

public interface IPhotoService
{
	Task<IEnumerable<Photo>> GetPhotosAsync();

	Task<Photo> GetPhotoByIdAsync(int id);

	Task<Photo> AddPhotoAsync(Photo photo);

	Task<Photo> UpdatePhotoAsync(Photo photo);

	Task DeletePhotoAsync(int id);
}
