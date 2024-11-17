namespace LuxuryPaintJohnsonAPI.Models.Dtos;

public class PhotoDto
{
	public int Id { get; set; }
	public string? Title { get; set; }
	public string? Url { get; set; }
	public string? Description { get; set; }
	public DateTime CreatedAt { get; set; }
	public int ProjectId { get; set; }
}
