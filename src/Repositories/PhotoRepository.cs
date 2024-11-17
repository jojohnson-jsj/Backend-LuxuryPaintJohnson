using LuxuryPaintJohnsonAPI.Data;
using LuxuryPaintJohnsonAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LuxuryPaintJohnsonAPI.Repositories;

public class PhotoRepository : IPhotoRepository
{
	private readonly AppDbContext context;
	
	public PhotoRepository(AppDbContext context)
	{
		this.context = context;
	}

	public async Task<IEnumerable<Photo>> GetPhotosAsync()
	{
		return await this.context.Photos.ToListAsync();
	}

	public async Task<Photo> GetPhotoByIdAsync(int id)
	{
		return await this.context.Photos.FindAsync(id) ?? new Photo
		{
			Id = 0,
			Title = "Default Photo",
			Url = "https://via.placeholder.com/300",
			Description = "No description available",
			CreatedAt = DateTime.MinValue
		};
	}

	public async Task<Photo> AddPhotoAsync(Photo photo)
	{
		await this.context.Photos.AddAsync(photo);
		await context.SaveChangesAsync();

		return photo;
	}

	public async Task<Photo> UpdatePhotoAsync(Photo photo)
	{
		this.context.Photos.Update(photo);
		await this.context.SaveChangesAsync();

		return photo;
	}

	public async Task DeletePhotoAsync(int id)
	{
		var photo = await this.context.Photos.FindAsync(id);
		
		if (photo is not null)
		{
			this.context.Photos.Remove(photo);
			await this.context.SaveChangesAsync();
		}
	}
}
