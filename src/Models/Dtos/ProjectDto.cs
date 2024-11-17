namespace LuxuryPaintJohnsonAPI.Models.Dtos;

public class ProjectDto
{
	public int Id { get; set; }
	public string? Title { get; set; }
	public DateTime CreatedAt { get; set; }
	public IEnumerable<PhotoDto>? Photos { get; set; }
}