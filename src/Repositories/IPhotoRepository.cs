using LuxuryPaintJohnsonAPI.Models;

namespace LuxuryPaintJohnsonAPI.Repositories;

public interface IPhotoRepository
{
	Task<IEnumerable<Photo>> GetPhotosAsync();

	Task<Photo> GetPhotoByIdAsync(int id);

	Task<Photo> AddPhotoAsync(Photo photo);

	Task<Photo> UpdatePhotoAsync(Photo photo);

	Task DeletePhotoAsync(int id);
}
