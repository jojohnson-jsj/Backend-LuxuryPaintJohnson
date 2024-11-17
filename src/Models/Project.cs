namespace LuxuryPaintJohnsonAPI.Models;

public class Project
{
	public int Id { get; set; }
	public string? Title { get; set; }
	public DateTime CreatedAt { get; set; }
	public IEnumerable<Photo>? Photos { get; set; }
}
